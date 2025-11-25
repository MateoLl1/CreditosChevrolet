using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditosChevrolet.Data;
using CreditosChevrolet.Models;
using CreditosChevrolet.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CreditosChevrolet.Repository
{
  public class RespuestaCreditoFinancieraRepository : IRespuestaCreditoFinancieraRepository
  {
    private readonly ApplicationDbContext _db;

    public RespuestaCreditoFinancieraRepository(ApplicationDbContext db)
    {
      _db = db;
    }

    public async Task AddAsync(RespuestaCreditoFinanciera respuesta)
    {
      await _db.RespuestasCredito.AddAsync(respuesta);
    }

    public Task UpdateAsync(RespuestaCreditoFinanciera respuesta)
    {
      _db.RespuestasCredito.Update(respuesta);
      return Task.CompletedTask;
    }

    public async Task<IEnumerable<RespuestaCreditoFinanciera>> GetBySolicitudIdAsync(int solicitudId)
    {
      return await _db.RespuestasCredito
          .Where(r => r.SolicitudCreditoId == solicitudId)
          .OrderByDescending(r => r.FechaRespuesta)
          .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
      await _db.SaveChangesAsync();
    }
  }
}
