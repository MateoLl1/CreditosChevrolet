using AutoMapper;
using CreditosChevrolet.Models;
using CreditosChevrolet.Models.Dtos;

namespace CreditosChevrolet.Mapping
{
  public class CreditoProfile : Profile
  {
    public CreditoProfile()
    {
      CreateMap<RespuestaCreditoFinanciera, RespuestaCreditoItemDto>();
    }
  }
}
