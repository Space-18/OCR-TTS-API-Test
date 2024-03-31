using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OCR.Presentation.Middlewares;

namespace OCR.Presentation
{
    public static class PresentationServicesRegistration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UsePresentationMiddlewares(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
            return application;
        }
    }
}
