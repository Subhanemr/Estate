using Estate.Application.Abstractions.Services;
using Estate.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Estate.Infrastructure.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
