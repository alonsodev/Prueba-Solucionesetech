using Solucionesetech.API.Middlewares;
using Solucionesetech.API.Models;
using Microsoft.AspNetCore.Builder;
using System.Net;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace Solucionesetech.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger Log)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Write(LogEventLevel.Error, $"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
