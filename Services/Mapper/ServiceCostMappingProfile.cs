using AutoMapper;
using Domain;
using Services.Models.OtherModels;

namespace Services.Mapper;

public class ServiceCostMappingProfile : Profile
{
    public ServiceCostMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<CostModel, Cost>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
        
        
        
        // Domain models -> Response models
        CreateMap<Cost, CostModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
    }
}