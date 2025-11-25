using System.Threading.Tasks;

namespace CreditosChevrolet.Services.Interfaces
{
  public interface IEmailService
  {
    Task SendAsync(string subject, string body);
  }
}
