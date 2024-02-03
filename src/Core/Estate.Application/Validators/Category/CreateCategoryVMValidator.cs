using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Category
{
    public class CreateCategoryVMValidator : AbstractValidator<CreateCategoryVM>
    {
        public CreateCategoryVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
