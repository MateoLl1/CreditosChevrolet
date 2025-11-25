using System.Threading.Tasks;
using CreditosChevrolet.Models;

namespace CreditosChevrolet.Repository.IRepository
{
  public interface INotificacionAsesorRepository
  {
    Task AddAsync(NotificacionAsesor notificacion);
    Task SaveChangesAsync();
  }
}
