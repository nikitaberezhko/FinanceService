using AutoMapper;
using FinanceService.Contracts.ApiModels;
using Services.Models.OtherModels;

namespace WebApi.Mapper;

public class ApiRevenueMappingProfile : Profile
{
    public ApiRevenueMappingProfile()
    {
        // Request -> Request models
        CreateMap<RevenueApiModel, RevenueModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
        
        
        // Response models -> Responses
        CreateMap<RevenueModel, RevenueApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
    }
}