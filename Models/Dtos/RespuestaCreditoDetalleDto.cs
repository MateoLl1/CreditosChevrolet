using System;
using System.Collections.Generic;

namespace CreditosChevrolet.Models.Dtos
{
  public class RespuestaCreditoDetalleDto
  {
    public string NumeroSolicitud { get; set; } = string.Empty;
    public int AsesorId { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public List<RespuestaCreditoItemDto> Respuestas { get; set; } = new List<RespuestaCreditoItemDto>();
  }
}
