using AutoMapper;
using Domain;
using OrderApiModels;
using OrderApiModels.ApiModels;
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
        await validator.Validate(model);

        var response = await orderApiClient.GetOrdersInPeriod(new GetOrdersInPeriodRequest
        {
            End = model.EndDate.ToDateTime(new TimeOnly(0, 0)),
            Period = model.Period
        });

        var orders = response.Data.Orders;
        var reportModel = await CreateReport(model.EndDate, orders);
        await reportRepository.Add(mapper.Map<Report>(reportModel));
        
        return reportModel;
    }

    public async Task Delete(DeleteReportModel model)
    {
        await validator.Validate(model);
        await reportRepository.Delete(mapper.Map<Report>(model));
    }
    
    public async Task<List<ReportModel>> GetShortenedList()
    {
        var reports =await reportRepository.GetShortenedList();
        var result = mapper.Map<List<ReportModel>>(reports);

        return result;
    }
    
    public async Task<ReportModel> GetById(GetReportByIdModel model)
    {
        await validator.Validate(model);
        var report = await reportRepository.GetById(mapper.Map<Report>(model));
        var result = mapper.Map<ReportModel>(report);
        
        return result;
    }
    
    
    private Task<ReportModel> CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    {
        var report = new ReportModel
        {
            Date = date,
            Revenues = orders.Select(x => new RevenueModel
            {
                Id = Guid.NewGuid(),
                Amount = x.Price,
                OrderId = x.Id
            }).ToList(),
            Costs = orders.Select(x => new CostModel
            {
                Id = Guid.NewGuid(),
                Amount = x.Costs,
                OrderId = x.Id
            }).ToList(),
            TotalRevenue = orders.Sum(x => x.Price),
            TotalCost = orders.Sum(x => x.Costs)
        };

        return Task.FromResult(report);
    }
}