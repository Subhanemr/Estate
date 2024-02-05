using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Settings
{
    public class UpdateSettingsVMValidator : AbstractValidator<UpdateSettingsVM>
    {
        public UpdateSettingsVMValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required")
                .Length(1, 1500).WithMessage("Name max characters is 1-1500");
        }
    }
}
