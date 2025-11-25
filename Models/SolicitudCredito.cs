using System;
using System.Collections.Generic;

namespace CreditosChevrolet.Models
{
  public class SolicitudCredito
  {
    public int Id { get; set; }
    public string NumeroSolicitud { get; set; } = string.Empty;
    public int AsesorId { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public int? FinancieraId { get; set; }
    public int? MinutosReintento { get; set; }
    public ICollection<RespuestaCreditoFinanciera> Respuestas { get; set; } = new List<RespuestaCreditoFinanciera>();
  }
}
