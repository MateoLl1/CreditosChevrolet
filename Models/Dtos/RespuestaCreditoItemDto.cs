using System;

namespace CreditosChevrolet.Models.Dtos
{
  public class RespuestaCreditoItemDto
  {
    public string Estado { get; set; } = string.Empty;
    public decimal? MontoAprobado { get; set; }
    public int? PlazoMeses { get; set; }
    public decimal? Tasa { get; set; }
    public DateTime FechaRespuesta { get; set; }
    public string? Observaciones { get; set; }
    public string JsonCompleto { get; set; } = string.Empty;
  }
}
