using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CreditosChevrolet.Models;
using CreditosChevrolet.Models.Dtos;
using CreditosChevrolet.Repository.IRepository;
using CreditosChevrolet.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CreditosChevrolet.Services
{
  public class CreditoService : ICreditoService
  {
    private readonly ISolicitudCreditoRepository _solicitudRepo;
    private readonly IRespuestaCreditoFinancieraRepository _respuestaRepo;
    private readonly INotificacionAsesorRepository _notificacionRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreditoService> _logger;

    public CreditoService(
        ISolicitudCreditoRepository solicitudRepo,
        IRespuestaCreditoFinancieraRepository respuestaRepo,
        INotificacionAsesorRepository notificacionRepo,
        IMapper mapper,
        ILogger<CreditoService> logger)
    {
      _solicitudRepo = solicitudRepo;
      _respuestaRepo = respuestaRepo;
      _notificacionRepo = notificacionRepo;
      _mapper = mapper;
      _logger = logger;
    }

    public async Task<bool> ProcesarRespuestaAsync(RespuestaCreditoRequestDto dto)
    {
      var solicitud = await _solicitudRepo.GetByNumeroSolicitudAsync(dto.NumeroSolicitud);
      if (solicitud == null)
      {
        _logger.LogWarning("Solicitud {NumeroSolicitud} no encontrada", dto.NumeroSolicitud);
        return false;
      }

      var respuesta = new RespuestaCreditoFinanciera
      {
        SolicitudCreditoId = solicitud.Id,
        Estado = dto.Estado,
        MontoAprobado = dto.MontoAprobado,
        PlazoMeses = dto.PlazoMeses,
        Tasa = dto.Tasa,
        FechaRespuesta = dto.FechaRespuesta,
        Observaciones = dto.Observacion,
        JsonCompleto = JsonSerializer.Serialize(dto)
      };

      await _respuestaRepo.AddAsync(respuesta);

      var mensajeNotificacion = $"La financiera respondió {dto.Estado} para la solicitud {dto.NumeroSolicitud}.";
      var notificacion = new NotificacionAsesor
      {
        AsesorId = solicitud.AsesorId,
        SolicitudCreditoId = solicitud.Id,
        Mensaje = mensajeNotificacion,
        Fecha = DateTime.UtcNow,
        Leido = false
      };

      await _notificacionRepo.AddAsync(notificacion);

      await _respuestaRepo.SaveChangesAsync();

      _logger.LogInformation("Respuesta procesada para solicitud {NumeroSolicitud} con estado {Estado}", dto.NumeroSolicitud, dto.Estado);

      return true;
    }

    public async Task<RespuestaCreditoDetalleDto?> ObtenerDetallePorNumeroSolicitudAsync(string numeroSolicitud)
    {
      var solicitud = await _solicitudRepo.GetByNumeroSolicitudAsync(numeroSolicitud);
      if (solicitud == null)
      {
        return null;
      }

      var respuestas = await _respuestaRepo.GetBySolicitudIdAsync(solicitud.Id);
      var respuestasDto = respuestas
          .Select(r => _mapper.Map<RespuestaCreditoItemDto>(r))
          .ToList();

      var detalle = new RespuestaCreditoDetalleDto
      {
        NumeroSolicitud = solicitud.NumeroSolicitud,
        AsesorId = solicitud.AsesorId,
        FechaSolicitud = solicitud.FechaSolicitud,
        Respuestas = respuestasDto
      };

      return detalle;
    }
  }
}
