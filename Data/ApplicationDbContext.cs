using Microsoft.EntityFrameworkCore;

namespace CreditosChevrolet.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


  }
}
