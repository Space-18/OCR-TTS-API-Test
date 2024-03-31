using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OCR.Application.Contracts.Persistence;
using OCR.Persistence.Contracts.Repositories;

namespace OCR.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlServer")!);
            });

            services.AddScoped<IUnitOfWorkP, UnitOfWorkP>();

            return services;
        }
    }
}
