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

            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}
