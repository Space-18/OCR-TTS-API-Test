using Microsoft.Extensions.DependencyInjection;
using OCR.Application.Contracts.Services;
using OCR.Infrastructure.Contracts.Services;

namespace OCR.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkS, UnitOfWorkS>();

            return services;
        }
    }
}
