using Estate.Application.ViewModels.ProductComment;
using FluentValidation;

namespace Estate.Application.Validators.ProductComment
{
    public class UpdateProductCommentVMValidator : AbstractValidator<UpdateProductCommentVM>
    {
        public UpdateProductCommentVMValidator()
        {
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required")
                .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
    }
}
