﻿using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Client
{
    public class UpdateClientVMValidator : AbstractValidator<UpdateClientVM>
    {
        public UpdateClientVMValidator()
        {
            RuleFor(x => x.CorporateId).GreaterThan(0).WithMessage("Corporate must be greater than 0");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required")
                .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required");
        }
    }
}
