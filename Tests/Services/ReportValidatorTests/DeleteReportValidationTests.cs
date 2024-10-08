using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportValidatorTests;

public class DeleteReportValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateValidatorForDeleteReportModel();
        var model = new DeleteReportModel { Id = Guid.NewGuid() };

        // Act
        var result = await validator.ValidateAsync(model);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Invalid()
    {
        // Arrange
        var validator = CreateValidatorForDeleteReportModel();
        var model = new DeleteReportModel
        {
            Id = Guid.Empty,
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    private ReportValidator CreateValidatorForDeleteReportModel() =>
        new(new Mock<IValidator<GenerateReportModel>>().Object,
            Provider.Get<IValidator<DeleteReportModel>>(),
            new Mock<IValidator<GetAllReportsModel>>().Object,
            new Mock<IValidator<GetReportByIdModel>>().Object);
}