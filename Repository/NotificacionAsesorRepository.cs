using System.Threading.Tasks;
using CreditosChevrolet.Data;
using CreditosChevrolet.Models;
using CreditosChevrolet.Repository.IRepository;

namespace CreditosChevrolet.Repository
{
  public class NotificacionAsesorRepository : INotificacionAsesorRepository
  {
    private readonly ApplicationDbContext _db;

    public NotificacionAsesorRepository(ApplicationDbContext db)
    {
      _db = db;
    }

    public async Task AddAsync(NotificacionAsesor notificacion)
    {
      await _db.NotificacionesAsesor.AddAsync(notificacion);
    }

    public async Task SaveChangesAsync()
    {
      await _db.SaveChangesAsync();
    }
  }
}
