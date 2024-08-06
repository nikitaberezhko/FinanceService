using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetReportByIdValidator : AbstractValidator<GetReportByIdModel>
{
    public GetReportByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}