using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Domain.Entities;
using Estate.Infrastructure.Implementations;
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

            services.AddScoped<IEmailService, EmailService>();

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
            services.AddScoped<ISettingsService, SettingsService>();

            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IAgencyAppUserRepository, AgencyAppUserRepository>();
            services.AddScoped<IAppUserImageRepository, AppUserImageRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICorporateRepository, CorporateRepository>();
            services.AddScoped<IExteriorTypeRepository, ExteriorTypeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            services.AddScoped<IParkingTypeRepository, ParkingTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoofTypeRepository, RoofTypeRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IViewTypeRepository, ViewTypeRepository>();

            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}
