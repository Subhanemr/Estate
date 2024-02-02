using Estate.Application.ViewModels.ViewType;
using FluentValidation;

namespace Estate.Application.Validators.ViewType
{
    public class UpdateViewTypeVMValidator : AbstractValidator<UpdateViewTypeVM>
    {
        public UpdateViewTypeVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
        }
    }
}
