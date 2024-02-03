using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Corporate
{
    public class CreateCorporateVMValidator : AbstractValidator<CreateCorporateVM>
    {
        public CreateCorporateVMValidator()
        {
            RuleFor(x => x.CorporateLink)
                .NotEmpty().WithMessage("Corporate link is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(25, 250).WithMessage("Description max characters is 25-200")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Biography can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
