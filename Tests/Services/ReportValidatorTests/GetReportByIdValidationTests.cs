using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportValidatorTests;

public class GetReportByIdValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateReportValidatorForGetReportById();
        var model = new GetReportByIdModel { Id = Guid.NewGuid() };

        // Act
        var result = await validator.ValidateAsync(model);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Invalid()
    {
        // Arrange
        var validator = CreateReportValidatorForGetReportById();
        var model = new GetReportByIdModel { Id = Guid.Empty };
        
        // Act

        // Assert   
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    private ReportValidator CreateReportValidatorForGetReportById() =>
        new(new Mock<IValidator<GenerateReportModel>>().Object,
            new Mock<IValidator<DeleteReportModel>>().Object,
            new Mock<IValidator<GetAllReportsModel>>().Object,
            Provider.Get<IValidator<GetReportByIdModel>>());
}