using AutoMapper;
using Domain;
using Moq;
using Services.Models.Request;
using Services.RefitClients.Interfaces;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportServiceTests;

public class GetShortenedListCasesTests
{
    [Fact]
    public async Task GetShortenedListCase_Should_Return_Not_Empty_List()
    {
        // Arrange
        var service = CreateReportServiceForGetShortenedList();
        var model = new GetAllReportsModel
        {
            Page = 1,
            PageSize = 10
        };
        
        // Act
        var actual = await service.GetShortenedList(model);
        
        // Assert
        Assert.NotEmpty(actual);
    }

    private ReportService CreateReportServiceForGetShortenedList()
    {
        var repository = new Mock<IReportRepository>();
        repository.Setup(x => x.GetShortenedList(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Report>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                TotalRevenue = 1000,
                TotalCost = 100
            },
            new()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                TotalRevenue = 1000,
                TotalCost = 100
            },
        });
        
        var service = new ReportService(repository.Object, 
            Provider.Get<IMapper>(),
            new Mock<IOrderApiClient>().Object,
            Provider.Get<ReportValidator>());

        return service;
    }
}