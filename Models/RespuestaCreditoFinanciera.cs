using System;

namespace CreditosChevrolet.Models
{
  public class RespuestaCreditoFinanciera
  {
    public int Id { get; set; }
    public int SolicitudCreditoId { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal? MontoAprobado { get; set; }
    public decimal? Tasa { get; set; }
    public DateTime FechaRespuesta { get; set; }
    public string? Observaciones { get; set; }
    public string JsonCompleto { get; set; } = string.Empty;

    public SolicitudCredito SolicitudCredito { get; set; } = null!;
  }
}
