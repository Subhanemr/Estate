using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.BlogReply
{
    public class CreateBlogReplyVMValidator : AbstractValidator<CreateBlogReplyVM>
    {
        public CreateBlogReplyVMValidator()
        {
            RuleFor(x => x.ReplyComment)
                .NotEmpty().WithMessage("Comment is required")
                .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
    }
}
