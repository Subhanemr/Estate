using Estate.Application.ViewModels.Account;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class RegisterVMValidator : AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .Length(2, 25).WithMessage("Username max characters is 2-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Username can only contain letters, numbers, and spaces");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must be entered mutled")
                .Length(10, 255).WithMessage("It should be between 10 and 255 characters")
                .EmailAddress().WithMessage("Invalid email address")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must be entered mutled")
                .Length(8, 25).WithMessage("Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Username can only contain letters, numbers, and spaces");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password must be entered mutled")
                .Length(8, 25).WithMessage("Password max characters is 8-25")
                .Equal(x => x.Password).WithMessage("The password must be the same")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Username can only contain letters, numbers, and spaces");
        }
    }
}
