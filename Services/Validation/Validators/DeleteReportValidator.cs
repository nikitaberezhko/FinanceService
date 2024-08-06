using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class DeleteReportValidator : AbstractValidator<DeleteReportModel>
{
    public DeleteReportValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}