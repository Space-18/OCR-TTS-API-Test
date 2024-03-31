using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OCR.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
