using AutoMapper;
using OrderService.Contracts.ApiModels;
using Services.Models.OtherModels;
using Services.Models.Response;
using Services.RefitClients.Interfaces;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;

namespace Services.Services.Implementations;

public class ReportService(
    IReportRepository reportRepository,
    IMapper mapper,
    IOrderApiClient orderApiClient,
    ReportValidator validator) 
    : BaseReportService(reportRepository, mapper, orderApiClient, validator), 
        IReportService
{
    protected override ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    {
        var report = new ReportModel
        {
            Date = date,
            Revenues = orders
                .AsParallel().Select(x => new RevenueModel
                {
                    Id = Guid.NewGuid(),
                    Amount = x.Price,
                    OrderId = x.Id
                }).ToList(),
            Costs = orders
                .AsParallel()
                .Where(x => x.Costs > 0)
                .Select(x => new CostModel
                {
                    Id = Guid.NewGuid(),
                    Amount = x.Costs,
                    OrderId = x.Id
                }).ToList(),
            TotalRevenue = orders.Sum(x => x.Price),
            TotalCost = orders.Sum(x => x.Costs)
        };

        return report;
    }
}