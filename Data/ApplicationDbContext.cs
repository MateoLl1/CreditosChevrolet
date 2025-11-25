using CreditosChevrolet.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditosChevrolet.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SolicitudCredito> SolicitudesCredito { get; set; }
    public DbSet<RespuestaCreditoFinanciera> RespuestasCredito { get; set; }
    public DbSet<NotificacionAsesor> NotificacionesAsesor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SolicitudCredito>()
          .HasIndex(s => s.NumeroSolicitud)
          .IsUnique();

      modelBuilder.Entity<RespuestaCreditoFinanciera>()
          .Property(r => r.MontoAprobado)
          .HasPrecision(18, 2);

      modelBuilder.Entity<RespuestaCreditoFinanciera>()
          .Property(r => r.Tasa)
          .HasPrecision(5, 2);

      modelBuilder.Entity<RespuestaCreditoFinanciera>()
          .HasOne(r => r.SolicitudCredito)
          .WithMany(s => s.Respuestas)
          .HasForeignKey(r => r.SolicitudCreditoId);

      modelBuilder.Entity<NotificacionAsesor>()
          .HasOne(n => n.SolicitudCredito)
          .WithMany()
          .HasForeignKey(n => n.SolicitudCreditoId);
    }
  }
}
