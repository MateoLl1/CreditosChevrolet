using System.Threading.Tasks;
using CreditosChevrolet.Models.Dtos;

namespace CreditosChevrolet.Services.Interfaces
{
  public interface ICreditoService
  {
    Task<bool> ProcesarRespuestaAsync(RespuestaCreditoRequestDto dto);
    Task<RespuestaCreditoDetalleDto?> ObtenerDetallePorNumeroSolicitudAsync(string numeroSolicitud);
  }
}
