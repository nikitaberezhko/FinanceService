using AutoMapper;
using FinanceService.Contracts.ApiModels;
using FinanceService.Contracts.Request;
using FinanceService.Contracts.Response;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Models.Response;

namespace WebApi.Mapper;

public class ApiReportMappingProfile : Profile
{
    public ApiReportMappingProfile()
    {
        // Request -> Request models
        CreateMap<DeleteReportRequest, DeleteReportModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));

        
        CreateMap<GetAllReportsRequest, GetAllReportsModel>()
            .ForMember(d => d.Page, map => map.MapFrom(c => c.Page))
            .ForMember(d => d.PageSize, map => map.MapFrom(c => c.PageSize));
        

        CreateMap<GenerateReportRequest, GenerateReportModel>()
            .ForMember(d => d.EndDate, map => map.MapFrom(c => c.EndDate))
            .ForMember(d => d.Period, map => map.MapFrom(c => c.Period));

        
        CreateMap<GetReportByIdRequest, GetReportByIdModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        
        // Response models -> Responses
        CreateMap<ReportModel, GenerateReportResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost))
            .ForMember(d => d.Revenues, map => map.MapFrom(c => c.Revenues))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs));
        
        
        CreateMap<ReportModel, GetReportByIdResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost))
            .ForMember(d => d.Revenues, map => map.MapFrom(c => c.Revenues))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs));
        
        
        CreateMap<ReportModel, ReportApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost))
            .ForMember(d => d.Revenues, map => map.MapFrom(c => c.Revenues))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs));
        
        
        CreateMap<CostModel, CostApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
        
        
        CreateMap<RevenueModel, RevenueApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Amount, map => map.MapFrom(c => c.Amount));
        
        
        CreateMap<ShortenedReportModel, ShortenedReportApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Date, map => map.MapFrom(c => c.Date))
            .ForMember(d => d.TotalRevenue, map => map.MapFrom(c => c.TotalRevenue))
            .ForMember(d => d.TotalCost, map => map.MapFrom(c => c.TotalCost));
    }
}