using Exceptions.Contracts.Services;
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
    public async Task<bool> ValidateAsync(GenerateReportModel model)
    {
        var validationResult = await generateReportValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
        
        return true;
    }

    public async Task<bool> ValidateAsync(DeleteReportModel model)
    {
        var validationResult = await deleteReportValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetAllReportsModel model)
    {
        var validationResult = await getAllReportsValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
        
        return true;
    }

    public async Task<bool> ValidateAsync(GetReportByIdModel model)
    {
        var validationResult = await getReportByIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            await ThrowWithStandartMessage();
        
        return true;

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