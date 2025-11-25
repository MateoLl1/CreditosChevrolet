using System;

namespace CreditosChevrolet.Models
{
  public class NotificacionAsesor
  {
    public int Id { get; set; }
    public int AsesorId { get; set; }
    public int SolicitudCreditoId { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public bool Leido { get; set; }

    public SolicitudCredito SolicitudCredito { get; set; } = null!;
  }
}
