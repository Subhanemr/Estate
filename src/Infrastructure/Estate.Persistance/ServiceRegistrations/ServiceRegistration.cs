using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Estate.Persistance.Implementations.Repositories;
using Estate.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estate.Persistance.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAgencyService, AgencyService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICorporateService, CorporateService>();
            services.AddScoped<IExteriorTypeService, ExteriorTypeService>();
            services.AddScoped<IFeaturesService, FeaturesService>();
            services.AddScoped<IParkingTypeService, ParkingTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoofTypeService, RoofTypeService>();
            services.AddScoped<IViewTypeService, ViewTypeService>();

            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IAgencyNameRepository, AgencyNameRepository>();
            services.AddScoped<IAgencyAppUserRepository, AgencyAppUserRepository>();
            services.AddScoped<IAppUserImageRepository, AppUserImageRepository>();
            services.AddScoped<IBlogNameRepository, BlogNameRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryNameRepository, CategoryNameRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICorporateNameRepository, CorporateNameRepository>();
            services.AddScoped<ICorporateRepository, CorporateRepository>();
            services.AddScoped<IExteriorTypeNameRepository, ExteriorTypeNameRepository>();
            services.AddScoped<IExteriorTypeRepository, ExteriorTypeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IFeaturesNameRepository, FeaturesNameRepository>();
            services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            services.AddScoped<IParkingTypeNameRepository, ParkingTypeNameRepository>();
            services.AddScoped<IParkingTypeRepository, ParkingTypeRepository>();
            services.AddScoped<IProductNameRepository, ProductNameRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoofTypeNameRepository, RoofTypeNameRepository>();
            services.AddScoped<IRoofTypeRepository, RoofTypeRepository>();
            services.AddScoped<IViewTypeNameRepository, ViewTypeNameRepository>();
            services.AddScoped<IViewTypeRepository, ViewTypeRepository>();

            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}
