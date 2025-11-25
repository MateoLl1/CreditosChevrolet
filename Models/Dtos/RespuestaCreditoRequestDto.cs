using System;

namespace CreditosChevrolet.Models.Dtos
{
  public class RespuestaCreditoRequestDto
  {
    public string NumeroSolicitud { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal? MontoAprobado { get; set; }
    public int? PlazoMeses { get; set; }
    public decimal? Tasa { get; set; }
    public string? Observacion { get; set; }
    public DateTime FechaRespuesta { get; set; }
  }
}
