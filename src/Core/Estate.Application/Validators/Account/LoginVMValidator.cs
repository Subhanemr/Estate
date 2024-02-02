using Estate.Application.ViewModels.Account;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username is required")
                .Length(2, 25).WithMessage("Username max characters is 2-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Username can only contain letters, numbers, and spaces");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 25).WithMessage("Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Password can only contain letters, numbers, and spaces");
        }
    }
}
