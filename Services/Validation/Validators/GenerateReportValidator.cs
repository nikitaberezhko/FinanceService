using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GenerateReportValidator : AbstractValidator<GenerateReportModel>
{
    public GenerateReportValidator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThan(new DateOnly(2020, 1, 1));
    }
}