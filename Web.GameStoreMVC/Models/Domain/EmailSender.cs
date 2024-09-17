using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Web.GameStoreMVC.Models.Domain
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
			_logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
			{
				Port = int.Parse(_configuration["Smtp:Port"]),
				Credentials = new NetworkCredential(_configuration["Smtp:Username"],
				_configuration["Smtp:Password"]),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(_configuration["Smtp:From"]),
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true
			};

			mailMessage.To.Add(email);

			return smtpClient.SendMailAsync(mailMessage);
		}
	}
}
