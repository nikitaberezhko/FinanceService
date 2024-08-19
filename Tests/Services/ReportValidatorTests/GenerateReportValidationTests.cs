using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportValidatorTests;

public class GenerateReportValidationTests
{
    [Fact]
    public async Task GenerateReport_MustBeValid()
    {
        // Arrange
        var validator = CreateValidatorForGenerateReportModel();
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 30
        };
        
        // Act
        var result = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GenerateReport_MustThrowServiceExceptionBecausePeriodLessThan1()
    {
        // Arrange
        var validator = CreateValidatorForGenerateReportModel();
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Period = 0
        };
        
        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task GenerateReport_MustThrowServiceExceptionBecauseEndDateLessThan2020_01_01()
    {
        // Arrange
        var validator = CreateValidatorForGenerateReportModel();
        var model = new GenerateReportModel
        {
            EndDate = DateOnly.FromDateTime(new DateTime(2019, 12, 31)),
            Period = 30
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    private ReportValidator CreateValidatorForGenerateReportModel() =>
        new(Provider.Get<IValidator<GenerateReportModel>>(),
            new Mock<IValidator<DeleteReportModel>>().Object,
            new Mock<IValidator<GetAllReportsModel>>().Object,
            new Mock<IValidator<GetReportByIdModel>>().Object);
}