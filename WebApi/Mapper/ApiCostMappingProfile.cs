using AutoMapper;
using Services.Models.OtherModels;
using WebApi.Models.ApiModels;

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