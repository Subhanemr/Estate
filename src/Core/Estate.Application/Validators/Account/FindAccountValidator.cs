using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class FindAccountValidator : AbstractValidator<FindAccountVM>
    {
        public FindAccountValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or Email is required")
                .Length(2, 255).WithMessage("Username or Email max characters is 2-255")
                .Matches(@"^(?:(?![@._])[a-zA-Z0-9@._-]{3,})$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid username or email format");
        }
    }
}
