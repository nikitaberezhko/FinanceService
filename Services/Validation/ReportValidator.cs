using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Models.Request;

namespace Services.Validation;

public class ReportValidator(
    IValidator<GenerateReportModel> generateReportValidator,
    IValidator<DeleteReportModel> deleteReportValidator,
    IValidator<GetAllReportsModel> getAllReportsValidator,
    IValidator<GetReportByIdModel> getReportByIdValidator)
{
    public async Task Validate(GenerateReportModel model)
    {
        var validationResult = await generateReportValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
    }

    public async Task Validate(DeleteReportModel model)
    {
        var validationResult = await deleteReportValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
    }
    
    public async Task Validate(GetAllReportsModel model)
    {
        var validationResult = await getAllReportsValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
    }

    public async Task Validate(GetReportByIdModel model)
    {
        var validationResult = await getReportByIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();

    }

    private Task ThrowWithStandartMessage()
    {
        throw new ServiceException
        {
            Title = "Model invalid",
            Message = "Model validation failed",
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}