using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Blog
{
    public class UpdateBlogVMValidator : AbstractValidator<UpdateBlogVM>
    {
        public UpdateBlogVMValidator()
        {
            RuleFor(x => x.ImageIds).NotNull().WithMessage("Image was not be emty");
            RuleForEach(x => x.ImageIds).GreaterThan(0).WithMessage("Image must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(2, 1500).WithMessage("Description max characters is 2-50")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Biography can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.FaceLink)
                .NotEmpty().WithMessage("Facebook is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.TwitLink)
                .NotEmpty().WithMessage("Twitter is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.GoogleLink)
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.LinkedLink)
                .NotEmpty().WithMessage("Linked-In is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.InstaLink)
                .NotEmpty().WithMessage("Instagram is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");
        }
    }
}
