using Estate.Application.ViewModels.BlogComment;
using FluentValidation;

namespace Estate.Application.Validators.BlogComment
{
    public class UpdateBlogCommentVMValidator : AbstractValidator<UpdateBlogCommentVM>
    {
        public UpdateBlogCommentVMValidator()
        {
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required")
                .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
    }
}
