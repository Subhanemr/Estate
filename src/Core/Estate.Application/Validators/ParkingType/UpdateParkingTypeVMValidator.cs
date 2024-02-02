﻿using Estate.Application.ViewModels.ParkingType;
using FluentValidation;

namespace Estate.Application.Validators.ParkingType
{
    public class UpdateParkingTypeVMValidator : AbstractValidator<UpdateParkingTypeVM>
    {
        public UpdateParkingTypeVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
        }
    }
}
