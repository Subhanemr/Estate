﻿using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.ExteriorType
{
    public class UpdateExteriorTypeVMValidator : AbstractValidator<UpdateExteriorTypeVM>
    {
        public UpdateExteriorTypeVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
        }
    }
}
