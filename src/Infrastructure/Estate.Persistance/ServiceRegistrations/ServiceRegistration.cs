using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Persistance.Contexts;
using Estate.Persistance.Implementations.Repositories;
using Estate.Persistance.Implementations.Services;
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
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IBlogImageRepository, BlogImageRepository>();
            services.AddScoped<IBlogNameRepository, BlogNameRepository>();
            services.AddScoped<IBlogReplyRepository, BlogReplyRepository>();
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
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();
            services.AddScoped<,>();


            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}
