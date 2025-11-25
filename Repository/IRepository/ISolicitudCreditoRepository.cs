using System.Threading.Tasks;
using CreditosChevrolet.Models;

namespace CreditosChevrolet.Repository.IRepository
{
  public interface ISolicitudCreditoRepository
  {
    Task<SolicitudCredito?> GetByNumeroSolicitudAsync(string numeroSolicitud);
    Task<bool> ExistsByNumeroSolicitudAsync(string numeroSolicitud);
  }
}
