﻿using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.BlogReply
{
    public class UpdateBlogReplyVMValidator : AbstractValidator<UpdateBlogReplyVM>
    {
        public UpdateBlogReplyVMValidator()
        {
            RuleFor(x => x.ReplyComment)
                .NotEmpty().WithMessage("Comment is required")
                .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
    }
}