using AutoMapper;
using Domain;
using Services.Models.OtherModels;

namespace Services.Mapper;

public class ServiceRevenueMappingProfile : Profile
{
    public ServiceRevenueMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<RevenueModel, Revenue>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));

        
        // Domain models -> Response models
        CreateMap<Revenue, RevenueModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
    }
}