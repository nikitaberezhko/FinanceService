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
    protected override ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    {
        var report = new ReportModel
        {
            Date = date,
            Revenues = orders
                .AsParallel()
                .Select(x => new RevenueModel
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

    #region Realization on parallel (not stable)

    // private object _lock = new();
    
    // protected override ReportModel CreateReport(DateOnly date, List<OrderApiFullModel> orders)
    // {
    //     Stopwatch sw = new();
    //     sw.Start();
    //     var report = new ReportModel
    //     {
    //         Date = date,
    //         Revenues = new List<RevenueModel>(),
    //         Costs = new List<CostModel>(),
    //         TotalRevenue = 0,
    //         TotalCost = 0
    //     };
    //     
    //     Thread[] threads = new Thread[2];
    //     threads[0] = new Thread(() => SummrizePart(orders.Take(orders.Count / 2).ToList()));
    //     threads[1] = new Thread(() => SummrizePart(orders.Skip(orders.Count / 2).ToList()));
    //
    //     foreach (var t in threads) 
    //         t.Start();
    //     foreach (var t in threads) 
    //         t.Join();
    //
    //     void SummrizePart(List<OrderApiFullModel> part)
    //     {
    //         for (int i = 0; i < part.Count; i++)
    //         {
    //             var revenue = new RevenueModel
    //             {
    //                 Amount = part[i].Price,
    //                 OrderId = part[i].Id,
    //                 Id = Guid.NewGuid()
    //             };
    //
    //             CostModel? cost = null;
    //             if (part[i].Costs > 0)
    //             {
    //                 cost = new CostModel
    //                 {
    //                     Amount = part[i].Costs,
    //                     OrderId = part[i].Id,
    //                     Id = Guid.NewGuid()
    //                 };
    //             }
    //             
    //             
    //             lock (_lock)
    //             {
    //                 report.Revenues.Add(revenue);
    //                 if (cost is not null) report.Costs.Add(cost);
    //                 report.TotalRevenue += part[i].Price;
    //                 report.TotalCost += part[i].Costs;
    //             }
    //         }
    //     }
    //
    //     sw.Stop();
    //     Console.WriteLine($"Report created in {sw.ElapsedTicks}ticks");
    //     
    //     return report;
    // }

    #endregion
}