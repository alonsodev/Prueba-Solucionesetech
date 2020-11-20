using Solucionesetech.CrossCutting.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.CrossCutting
{
    public class EmailHelper
    {
        private readonly IConfiguration _configuration;
        static readonly ILogger Log = Serilog.Log.ForContext<EmailHelper>();
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public bool SendEmail(string NameTo, string To, string Subject, string Body)
        {
            SmtpClient client = new SmtpClient();
            bool mail_sent = false;
            try
            {
                MimeMessage message = new MimeMessage();


                MailboxAddress from = new MailboxAddress(_configuration["MailKit:Smtp.From.Name"], _configuration["MailKit:Smtp.From.Email"]);
                message.From.Add(from);

               

                MailboxAddress to = new MailboxAddress(NameTo, To);
                message.To.Add(to);

                message.Subject = Subject;


                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = Body;
                //bodyBuilder.TextBody = "Hello World!";


                message.Body = bodyBuilder.ToMessageBody();



                client.Connect(_configuration["MailKit:Smtp.Host"], int.Parse(_configuration["MailKit:Smtp.Port"]), SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_configuration["MailKit:Smtp.User"], _configuration["MailKit:Smtp.Password"]);

                client.Send(message);
                client.Disconnect(true);
                mail_sent = true;
                Log.Write(LogEventLevel.Information, String.Format("Enviar Notificacion: FROM:{0}, TO:{1}, CC:{2}, BCC:{3}, SUBJECT:{4}", _configuration["MailKit:Smtp.From.Email"], To, "", "", Subject));
            }

            catch (Exception ex)
            {
                mail_sent = false;
                throw ex;
            }
            finally
            {
                client.Dispose();
            }

            return mail_sent;

        }
    }
}
