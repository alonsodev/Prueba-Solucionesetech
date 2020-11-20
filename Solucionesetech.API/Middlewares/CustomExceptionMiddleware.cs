
using Solucionesetech.API.Models;
using Solucionesetech.CrossCutting;
using Solucionesetech.CrossCutting.Common;
using Solucionesetech.Dtos.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IO;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.API.Middlewares
{

    public class ExceptionMiddleware
    {
        const string MessageTemplate =
          "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        

        private readonly RequestDelegate _next;

        static readonly ILogger Log = Serilog.Log.ForContext<ExceptionMiddleware>();

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public ExceptionMiddleware(RequestDelegate next)
        {

            _next = next;
            
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var sw = Stopwatch.StartNew();
            var logRequestBody = string.Empty;
            try
            {
                sw.Stop();

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 399 ? LogEventLevel.Error : LogEventLevel.Information;

                var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;


                log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);




                logRequestBody = await LogRequest(httpContext, LogEventLevel.Information);

                await _next(httpContext);
            }
            catch (Exception ex)

            {


                LogException(httpContext, sw, ex, logRequestBody);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex, string logRequestBody)
        {
            sw.Stop();
          // EmailHelper.SendEmailError(ex.Message + " " + ex.InnerException + " " + ex.StackTrace, _options);
            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate + Environment.NewLine + logRequestBody, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

            return true;
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            System.Console.WriteLine(exception.StackTrace.ToString());

            var errorCode = "error";
            var statusCode = HttpStatusCode.InternalServerError;

            var exceptionType = exception.GetType();
            switch (exception)
            {


                case SETECHApplicationException e when exceptionType == typeof(SETECHApplicationException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = "Internal Server Error";
                    break;
            }


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = errorCode
            }.ToString());
        }

        static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = Log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }



        private async Task<string> LogRequest(HttpContext context, LogEventLevel level)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            var logRequestBody = $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme}{Environment.NewLine}" +
                                   $"Host: {context.Request.Host}{Environment.NewLine}" +
                                   $"Path: {context.Request.Path}{Environment.NewLine}" +
                                   $"QueryString: {context.Request.QueryString}{Environment.NewLine}" +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}";
            context.Request.Body.Position = 0;

            return logRequestBody;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
