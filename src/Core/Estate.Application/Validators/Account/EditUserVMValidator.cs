using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class EditUserVMValidator : AbstractValidator<EditUserVM>
    {
        public EditUserVMValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .Length(2, 25).WithMessage("Username max characters is 2-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Username can only contain letters, numbers, and spaces");
        }
    }
}
