using System.Diagnostics;
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
    private object _lock = new();

    #region OnlyLinq

    // protected override ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    // {
    //     Stopwatch sw = new Stopwatch();
    //     sw.Start();
    //     var report = new ReportModel
    //     {
    //         Date = date,
    //         Revenues = orders
    //             .AsParallel().Select(x => new RevenueModel
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Amount = x.Price,
    //                 OrderId = x.Id
    //             }).ToList(),
    //         Costs = orders
    //             .AsParallel()
    //             .Where(x => x.Costs > 0)
    //             .Select(x => new CostModel
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Amount = x.Costs,
    //                 OrderId = x.Id
    //             }).ToList(),
    //         TotalRevenue = orders.Sum(x => x.Price),
    //         TotalCost = orders.Sum(x => x.Costs)
    //     };
    //
    //     sw.Stop();
    //     Console.WriteLine($"ReportService: {sw.ElapsedMilliseconds}");
    //     return report;
    // }

    #endregion
    
    protected override ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        
        double totalRevenue = 0;
        double totalCost = 0;
    
        
        Thread thread1 = new Thread(SummarizeRevenues);
        Thread thread2 = new Thread(SummarizeCosts);
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
        
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
            TotalRevenue = totalRevenue,
            TotalCost = totalCost
        };
        sw.Stop();
        Console.WriteLine($"ReportService: {sw.ElapsedMilliseconds}");
        
        return report;
    
        void SummarizeRevenues()
        {
            lock (_lock)
            {
                foreach (var order in orders)
                {
                    totalRevenue += order.Price;
                }
            }
        }
        
        void SummarizeCosts()
        {
            lock (_lock)
            {
                foreach (var order in orders)
                {
                    totalCost += order.Costs;
                }
            }
        }
    }
}