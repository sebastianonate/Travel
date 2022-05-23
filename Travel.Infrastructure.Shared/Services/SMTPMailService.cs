using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Travel.Core.Application.DTOs.Mail;
using Travel.Core.Application.DTOs.Settings;
using Travel.Core.Application.Interfaces.Shared;

namespace Travel.Infrastructure.Shared.Services
{
    public class SMTPMailService : IMailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<SMTPMailService> _logger { get; }

        public SMTPMailService(IOptions<MailSettings> mailSettings, ILogger<SMTPMailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        
        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.From);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}