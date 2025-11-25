using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CreditosChevrolet.Configuration;
using CreditosChevrolet.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CreditosChevrolet.Services
{
  public class EmailService : IEmailService
  {
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
    {
      _settings = options.Value;
      _logger = logger;
    }

    public async Task SendAsync(string subject, string body)
    {
      if (string.IsNullOrWhiteSpace(_settings.SmtpServer) ||
          string.IsNullOrWhiteSpace(_settings.User) ||
          string.IsNullOrWhiteSpace(_settings.To))
      {
        _logger.LogWarning("EmailSettings no está configurado correctamente. User: {User}, To: {To}", _settings.User, _settings.To);
        return;
      }

      _logger.LogInformation("Preparando envío de correo al usuario {To}", _settings.To);

      using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort);
      client.EnableSsl = _settings.EnableSsl;
      client.Credentials = new NetworkCredential(_settings.User, _settings.Password);

      var fromAddress = string.IsNullOrWhiteSpace(_settings.From) ? _settings.User : _settings.From;

      using var message = new MailMessage();
      message.From = new MailAddress(fromAddress);
      message.To.Add(_settings.To);
      message.Subject = subject;
      message.Body = body;

      await client.SendMailAsync(message);

      _logger.LogInformation("Correo enviado al usuario {To}", _settings.To);
    }
  }
}
