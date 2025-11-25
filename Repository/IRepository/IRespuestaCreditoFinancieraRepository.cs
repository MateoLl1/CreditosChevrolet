using System.Collections.Generic;
using System.Threading.Tasks;
using CreditosChevrolet.Models;

namespace CreditosChevrolet.Repository.IRepository
{
  public interface IRespuestaCreditoFinancieraRepository
  {
    Task AddAsync(RespuestaCreditoFinanciera respuesta);
    Task UpdateAsync(RespuestaCreditoFinanciera respuesta);
    Task<IEnumerable<RespuestaCreditoFinanciera>> GetBySolicitudIdAsync(int solicitudId);
    Task SaveChangesAsync();
  }
}
