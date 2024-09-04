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

public class GetByIdCasesTests
{
    [Fact]
    public async Task GetByIdCase_Should_Return_ReportModel_With_Right_Id()
    {
        // Arrange
        var service = CreateReportServiceForGetById(out Guid expected);
        var model = new GetReportByIdModel
        {
            Id = expected
        };
        
        // Act
        var actual = await service.GetById(model);
        
        // Assert
        Assert.Equal(expected, actual.Id);
    }

    private ReportService CreateReportServiceForGetById(out Guid expectedId)
    {
        expectedId = Guid.NewGuid();
        var repository = new Mock<IReportRepository>();
        repository.Setup(x => x.GetById(It.IsAny<Report>())).ReturnsAsync(new Report
        {
            Id = expectedId
        });
        
        var service = new ReportService(repository.Object, 
            Provider.Get<IMapper>(),
            new Mock<IOrderApiClient>().Object,
            Provider.Get<ReportValidator>());

        return service;
    }
}