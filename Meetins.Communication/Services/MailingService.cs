using Meetins.Communication.Abstractions;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Meetins.Communication.Services
{
    public class MailingService : IMailingService
    {
        private readonly PostgreDbContext _postgreDbContext;
        private readonly IConfiguration _configuration;

        public MailingService(IConfiguration configuration, PostgreDbContext postgreDbContext)
        {

            _configuration = configuration;
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Метод отправит код подтверждения на почту.
        /// </summary>
        /// <param name="code">Код подтверждения.</param>
        /// <param name="emailTo">email на который отправить сообщение.</param>
        public async Task SendAcceptCodeMailAsync(string code, string emailTo)
        {
            try
            {
                var data = await GetHostAndPortAsync(emailTo);
                var email = _configuration.GetSection("MailingSettings:Email").Value;
                var password = _configuration.GetSection("MailingSettings:Password").Value;

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(email));
                emailMessage.To.Add(MailboxAddress.Parse(emailTo));
                emailMessage.Subject = "Meetins: Код подтверждения";

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "Код подтверждения: " + code
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(data.Item1, data.Item2, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(email, password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        private async Task<(string, int)> GetHostAndPortAsync(string param)
        {
            // Для mail.ru или bk.ru.
            if (param.Contains("@mail") || param.Contains("@bk") || param.Contains("@gmail") || param.Contains("@yandex"))
            {
                return await Task.FromResult(("smtp.mail.ru", 2525));
            }

            // Для gmail.com.
            // if (param.Contains("@gmail"))
            // {
            //     return await Task.FromResult(("smtp.gmail.com", 587));
            // }

            // Для yandex.ru.
            // if (param.Contains("@yandex"))
            // {
            //     return await Task.FromResult(("smtp.yandex.ru", 465));
            // }

            return (null, 0);
        }
    }
}
