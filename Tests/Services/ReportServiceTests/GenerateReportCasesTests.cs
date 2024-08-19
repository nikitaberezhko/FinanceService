using AutoMapper;
using CommonModel.Contracts;
using Domain;
using Moq;
using OrderService.Contracts.ApiModels;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;
using Services.Models.Request;
using Services.RefitClients.Interfaces;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportServiceTests;

public class GenerateReportCasesTests
{
    [Fact]
    public async Task GenerateCase_MustReturnReportModelWithNewId()
    {
        // Arrange
        var service = CreateReportServiceForGenerateReport(out _, out _);
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var actual = await service.Generate(model);

        // Assert
        Assert.NotEqual(Guid.Empty, actual.Id);
    }

    [Fact]
    public async Task GenerateCase_MustReturnReportModelWithRightCosts()
    {
        // Arrange
        var service = CreateReportServiceForGenerateReport(out var rightCosts, out _);
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var actual = await service.Generate(model);

        // Assert
        Assert.Equal(rightCosts, actual.Costs.Sum(x => x.Amount));
    }

    [Fact]
    public async Task GenerateCase_MustReturnReportModelWithRightTotalCost()
    {
        // Arrange
        var service = CreateReportServiceForGenerateReport(out var rightCosts, out _);
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var actual = await service.Generate(model);
        
        // Assert
        Assert.Equal(rightCosts, actual.TotalCost);
    }

    [Fact]
    public async Task GenerateCase_MustReturnReportModelWithRightRevenues()
    {
        // Arrange
        var service = CreateReportServiceForGenerateReport(out _, out var rightRevenue);
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var actual = await service.Generate(model);
        
        // Assert
        Assert.Equal(rightRevenue, actual.Revenues.Sum(x => x.Amount));
    }
    
    [Fact]
    public async Task GenerateCase_MustReturnReportModelWithRightTotalRevenue()
    {
        // Arrange
        var service = CreateReportServiceForGenerateReport(out _, out var rightRevenue);
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var actual = await service.Generate(model);
        
        // Assert
        Assert.Equal(rightRevenue, actual.TotalRevenue);
    }

    private ReportService CreateReportServiceForGenerateReport(out double rightCosts, out double rightRevenue)
    {
        var repository = new Mock<IReportRepository>();
        repository.Setup(x => x.Add(It.IsAny<Report>())).ReturnsAsync(Guid.NewGuid());
        
        var orderApiClient = new Mock<IOrderApiClient>();
        orderApiClient.Setup(x => x.GetOrdersInPeriod(It.IsAny<GetOrdersInPeriodRequest>()))
            .ReturnsAsync(GenerateResponseFromOrderApi(out rightCosts, out rightRevenue));
        
        var service = new ReportService(repository.Object, 
            Provider.Get<IMapper>(),
            orderApiClient.Object,
            Provider.Get<ReportValidator>());

        return service;
    }
    
    private CommonResponse<GetOrdersInPeriodResponse> GenerateResponseFromOrderApi(
        out double rightCosts, out double rightRevenue)
    {
        var orders = new List<OrderApiFullModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Price = 1000,
                Costs = 100,
                ClientId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Price = 1000,
                Costs = 100,
                ClientId = Guid.NewGuid()
            }
        };
        
        rightRevenue = orders.Sum(x => x.Price);
        rightCosts = orders.Sum(x => x.Costs);
        
        return new CommonResponse<GetOrdersInPeriodResponse>
        {
            Data = new GetOrdersInPeriodResponse
            {
                Orders = orders
            }
        };
    }
}