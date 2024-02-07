using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or Email is required")
                .Length(2, 255).WithMessage("Username or Email max characters is 2-255")
                .Matches(@"^(?:(?![@._])[a-zA-Z0-9@._-]{3,})$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid username or email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 25).WithMessage("Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Password can only contain letters, numbers, and spaces");
        }
    }
}
