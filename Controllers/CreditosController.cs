using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreditosChevrolet.Models.Dtos;
using CreditosChevrolet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CreditosChevrolet.Controllers
{
  [ApiController]
  [Route("api/creditos")]
  public class CreditosController : ControllerBase
  {
    private static readonly HashSet<string> EstadosPermitidos = new HashSet<string>
        {
            "APROBADO",
            "NEGADO",
            "CONDICIONADO",
            "REQUIERE_DOCUMENTOS",
            "EN_PROCESO"
        };

    private readonly ICreditoService _creditoService;
    private readonly ILogger<CreditosController> _logger;

    public CreditosController(ICreditoService creditoService, ILogger<CreditosController> logger)
    {
      _creditoService = creditoService;
      _logger = logger;
    }

    [HttpPost("respuesta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RecibirRespuesta([FromBody] RespuestaCreditoRequestDto dto)
    {
      if (dto == null)
      {
        return BadRequest("El cuerpo de la solicitud es obligatorio.");
      }

      if (string.IsNullOrWhiteSpace(dto.NumeroSolicitud))
      {
        return BadRequest("El número de solicitud es obligatorio.");
      }

      if (string.IsNullOrWhiteSpace(dto.Estado))
      {
        return BadRequest("El estado es obligatorio.");
      }

      dto.Estado = dto.Estado.Trim().ToUpperInvariant();

      if (!EstadosPermitidos.Contains(dto.Estado))
      {
        return BadRequest("El estado ingresado no es válido.");
      }

      if (dto.Estado == "APROBADO")
      {
        if (!dto.MontoAprobado.HasValue)
        {
          return BadRequest("El monto aprobado es obligatorio cuando el estado es APROBADO.");
        }

        if (!dto.PlazoMeses.HasValue)
        {
          return BadRequest("El plazo en meses es obligatorio cuando el estado es APROBADO.");
        }
      }

      if (dto.Estado == "CONDICIONADO")
      {
        if (string.IsNullOrWhiteSpace(dto.Observacion))
        {
          return BadRequest("La observación es obligatoria cuando el estado es CONDICIONADO.");
        }
      }

      if (dto.Estado == "NEGADO")
      {
        if (string.IsNullOrWhiteSpace(dto.Observacion))
        {
          return BadRequest("La observación es obligatoria cuando el estado es NEGADO.");
        }
      }

      if (dto.FechaRespuesta == DateTime.MinValue)
      {
        dto.FechaRespuesta = DateTime.UtcNow;
      }

      try
      {
        var procesado = await _creditoService.ProcesarRespuestaAsync(dto);

        if (!procesado)
        {
          return NotFound($"No se encontró la solicitud con número {dto.NumeroSolicitud}.");
        }

        return Ok(new { mensaje = "Respuesta procesada correctamente." });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error al procesar la respuesta de crédito para la solicitud {NumeroSolicitud}", dto.NumeroSolicitud);
        return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");
      }
    }

    [HttpGet("respuesta/{numeroSolicitud}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerRespuesta(string numeroSolicitud)
    {
      if (string.IsNullOrWhiteSpace(numeroSolicitud))
      {
        return BadRequest("El número de solicitud es obligatorio.");
      }

      var detalle = await _creditoService.ObtenerDetallePorNumeroSolicitudAsync(numeroSolicitud);

      if (detalle == null)
      {
        return NotFound($"No se encontró información para la solicitud {numeroSolicitud}.");
      }

      return Ok(detalle);
    }
  }
}
