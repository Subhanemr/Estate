using Estate.Application.ViewModels;
using FluentValidation;

namespace Estate.Application.Validators.Product
{
    public class CreateProductVMValidator : AbstractValidator<CreateProductVM>
    {
        public CreateProductVMValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");

            RuleFor(x => x.FeatureIds).NotNull().WithMessage("Feature was not be emty");
            RuleForEach(x => x.FeatureIds).GreaterThan(0).WithMessage("Feature must be greater than 0");

            RuleFor(x => x.ExteriorTypeIds).NotNull().WithMessage("Exterior Type was not be emty");
            RuleForEach(x => x.ExteriorTypeIds).GreaterThan(0).WithMessage("Exterior Type must be greater than 0");

            RuleFor(x => x.ParkingTypeIds).NotNull().WithMessage("Parking Type was not be emty");
            RuleForEach(x => x.ParkingTypeIds).GreaterThan(0).WithMessage("Parking Type must be greater than 0");

            RuleFor(x => x.RoofTypeIds).NotNull().WithMessage("Roof Type was not be emty");
            RuleForEach(x => x.RoofTypeIds).GreaterThan(0).WithMessage("Roof Type must be greater than 0");

            RuleFor(x => x.ViewTypeIds).NotNull().WithMessage("View Type was not be emty");
            RuleForEach(x => x.ViewTypeIds).GreaterThan(0).WithMessage("View Type must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThanOrEqualTo(999999).WithMessage("Price must not be more than 999999");

            RuleFor(x => x.LotSize)
                .NotEmpty().WithMessage("Lot Size is required")
                .GreaterThan(0).WithMessage("Lot Size must be greater than 0")
                .LessThanOrEqualTo(9999).WithMessage("Lot Size must not be more than 9999");

            RuleFor(x => x.Area)
                .NotEmpty().WithMessage("Area is required")
                .GreaterThan(0).WithMessage("Area must be greater than 0")
                .LessThanOrEqualTo(9999).WithMessage("Area must not be more than 9999");

            RuleFor(x => x.Bedrooms)
                .NotNull().WithMessage("Bedrooms is required")
                .GreaterThan(0).WithMessage("Bedrooms must be greater than 0")
                .LessThanOrEqualTo(255).WithMessage("Bedrooms must not be more than 255");

            RuleFor(x => x.Bathrooms)
                .NotNull().WithMessage("Bathrooms is required")
                .GreaterThan(0).WithMessage("Bathrooms must be greater than 0")
                .LessThanOrEqualTo(255).WithMessage("Bathrooms must not be more than 255");

            RuleFor(x => x.Garages)
                .NotNull().WithMessage("Garages is required")
                .GreaterThan(0).WithMessage("Garages must be greater than 0")
                .LessThanOrEqualTo(255).WithMessage("Garages must not be more than 255");

            RuleFor(x => x.YearBuilt)
                .NotNull().WithMessage("Bathrooms is required")
                .GreaterThan(1000).WithMessage("Bathrooms must be greater than 1000")
                .LessThanOrEqualTo(2500).WithMessage("Bathrooms must not be more than 2500");

            RuleFor(x => x.RoomCount)
                .NotNull().WithMessage("Room Count is required")
                .GreaterThan(0).WithMessage("Room Count must be greater than 0")
                .LessThanOrEqualTo(255).WithMessage("Room Count must not be more than 255");

            RuleFor(x => x.SchoolDistrict)
                .Length(25, 200).WithMessage("School District max characters is 25-200")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("School District can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.SchoolDistrictPhone)
                .Length(5, 25).WithMessage("Phone max characters is 5-25")
                .Matches(@"^\+?[0-9]*$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(25, 250).WithMessage("Address max characters is 25-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Address can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.FaceLink)
                .NotEmpty().WithMessage("Facebook is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.TwitLink)
                .NotEmpty().WithMessage("Twitter is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.GoogleLink)
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(25, 1500).WithMessage("Description max characters is 25-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Description can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.MainPhoto)
                .NotEmpty().WithMessage("Main Image is required");

            RuleFor(x => x.Photos)
                .NotEmpty().WithMessage("Images is required");

            RuleFor(x => x.OrderDayOrMoth)
                .NotEmpty().WithMessage("Order Day or Moth is required");
        }
    }
}
