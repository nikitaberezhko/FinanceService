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

public class DeleteReportCasesTests
{
    [Fact]
    public async Task DeleteCase_Should_Return_ReportModel_With_Right_Id()
    {
        // Arrange
        var service = CreateReportServiceForDelete(out var expected);
        var model = new DeleteReportModel { Id = expected };
        
        // Act
        var actual = await service.Delete(model);
        
        // Assert
        Assert.Equal(expected, actual);
    }

    private ReportService CreateReportServiceForDelete(out Guid expected)
    {
        expected = Guid.NewGuid();
        var repository = new Mock<IReportRepository>();
        repository.Setup(x => x.Delete(It.IsAny<Report>())).ReturnsAsync(expected);
        var service = new ReportService(repository.Object, 
            Provider.Get<IMapper>(),
            new Mock<IOrderApiClient>().Object,
            Provider.Get<ReportValidator>());
        return service;
    }
}