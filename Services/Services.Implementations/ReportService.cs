using AutoMapper;
using Domain;
using OrderService.Contracts.ApiModels;
using OrderService.Contracts.Request;
using Services.Models.OtherModels;
using Services.Models.Request;
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
    ReportValidator validator) : IReportService
{
    public async Task<ReportModel> Generate(GenerateReportModel model)
    {
        await validator.ValidateAsync(model);

        var response = await orderApiClient.GetOrdersInPeriod(new GetOrdersInPeriodRequest
        {
            End = model.EndDate.ToDateTime(new TimeOnly(0, 0)).ToUniversalTime(),
            Period = model.Period
        });

        var orders = response.Data!.Orders;
        var result = CreateReport(model.EndDate, orders);
        var id = await reportRepository.Add(mapper.Map<Report>(result));
        result.Id = id;
        
        return result;
    }

    public async Task<Guid> Delete(DeleteReportModel model)
    {
        await validator.ValidateAsync(model);
        var id = await reportRepository.Delete(mapper.Map<Report>(model));
        
        return id;
    }
    
    public async Task<List<ShortenedReportModel>> GetShortenedList(GetAllReportsModel model)
    {
        await validator.ValidateAsync(model);
        var reports = await reportRepository.GetShortenedList(model.Page, model.PageSize);
        var result = mapper.Map<List<ShortenedReportModel>>(reports);

        return result;
    }
    
    public async Task<ReportModel> GetById(GetReportByIdModel model)
    {
        await validator.ValidateAsync(model);
        var report = await reportRepository.GetById(mapper.Map<Report>(model));
        var result = mapper.Map<ReportModel>(report);
        
        return result;
    }
    
    
    private ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
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