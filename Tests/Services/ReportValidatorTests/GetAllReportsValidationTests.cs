using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.ReportValidatorTests;

public class GetAllReportsValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateValidatorForGetAllReportsModel();
        var model = new GetAllReportsModel
        {
            Page = 1,
            PageSize = 10
        };

        // Act
        var result = await validator.ValidateAsync(model);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Page_Less_Than_1()
    {
        // Arrange
        var validator = CreateValidatorForGetAllReportsModel();
        var model = new GetAllReportsModel
        {
            Page = 0,
            PageSize = 10
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_PageSize_Less_Than_1()
    {
        // Arrange
        var validator = CreateValidatorForGetAllReportsModel();
        var model = new GetAllReportsModel
        {
            Page = 1,
            PageSize = 0
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_PageSize_More_Than_50()
    {
        // Arrange
        var validator = CreateValidatorForGetAllReportsModel();
        var model = new GetAllReportsModel
        {
            Page = 1,
            PageSize = 51
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    private ReportValidator CreateValidatorForGetAllReportsModel() =>
        new ReportValidator(new Mock<IValidator<GenerateReportModel>>().Object,
            new Mock<IValidator<DeleteReportModel>>().Object,
            Provider.Get<IValidator<GetAllReportsModel>>(),
            new Mock<IValidator<GetReportByIdModel>>().Object);
}