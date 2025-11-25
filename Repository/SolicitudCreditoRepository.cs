using System.Threading.Tasks;
using CreditosChevrolet.Data;
using CreditosChevrolet.Models;
using CreditosChevrolet.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CreditosChevrolet.Repository
{
  public class SolicitudCreditoRepository : ISolicitudCreditoRepository
  {
    private readonly ApplicationDbContext _db;

    public SolicitudCreditoRepository(ApplicationDbContext db)
    {
      _db = db;
    }

    public async Task<SolicitudCredito?> GetByNumeroSolicitudAsync(string numeroSolicitud)
    {
      return await _db.SolicitudesCredito
          .FirstOrDefaultAsync(s => s.NumeroSolicitud == numeroSolicitud);
    }

    public async Task<bool> ExistsByNumeroSolicitudAsync(string numeroSolicitud)
    {
      return await _db.SolicitudesCredito
          .AnyAsync(s => s.NumeroSolicitud == numeroSolicitud);
    }
  }
}
