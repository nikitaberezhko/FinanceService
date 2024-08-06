using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;

namespace Services.Mapper;

public class ServiceReportMappingProfile : Profile
{
    public ServiceReportMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<ReportModel, Report>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.Revenues, map => map.MapFrom(c => c.Revenues))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost));
        
        
        CreateMap<GetReportByIdModel, Report>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        CreateMap<DeleteReportModel, Report>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        
        // Domain models -> Response models
        CreateMap<Report, ReportModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.Revenues, map => map.MapFrom(c => c.Revenues))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost));
        
        
        CreateMap<Report, ShortenedReportModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost));
    }
}