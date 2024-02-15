using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class ResetPasswordVMValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordVMValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .Length(8, 25).WithMessage("New Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("New Password can only contain letters, numbers, and spaces");

            RuleFor(x => x.NewConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("New Confirm Password must match New Password")
                .Length(8, 25).WithMessage("New Confirm Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("New Confirm Password can only contain letters, numbers, and spaces");
        }
    }
}
