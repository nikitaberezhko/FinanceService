using AutoMapper;
using FinanceService.Contracts.ApiModels;
using Services.Models.OtherModels;

namespace WebApi.Mapper;

public class ApiCostMappingProfile : Profile
{
    public ApiCostMappingProfile()
    {
        // Request -> Request models
        CreateMap<CostApiModel, CostModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
        
        
        // Response models -> Responses
        CreateMap<CostModel, CostApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
    }
}