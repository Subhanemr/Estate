﻿using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Account
{
    public class UpdateAppUserAgentVMValidator : AbstractValidator<UpdateAppUserAgentVM>
    {
        public UpdateAppUserAgentVMValidator()
        {
            RuleFor(x => x.AgencyId).GreaterThan(0).WithMessage("Agency must be greater than 0");


            RuleFor(x => x.ImageIds).NotNull().WithMessage("Image was not be emty");
            RuleForEach(x => x.ImageIds).GreaterThan(0).WithMessage("Image must be greater than 0");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone is required")
                .Length(5, 25).WithMessage("Phone max characters is 5-25")
                .Matches(@"^\+?[0-9]*$").WithMessage("Invalid phone number format");

            RuleFor(x => x.PhoneSecond)
                .Length(5, 25).WithMessage("Phone max characters is 5-25")
                .Matches(@"^\+?[0-9]*$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(10, 300).WithMessage("Address max characters is 10-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Address can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.About)
               .NotEmpty().WithMessage("About is required")
               .Length(15, 1500).WithMessage("About max characters is 15-1500")
               .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("About can only contain letters, numbers, spaces, double quotes, commas, and periods.");

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

            RuleFor(x => x.TermsConditions)
                .NotEmpty().WithMessage("TermsConditions is required");
        }
    }
}
