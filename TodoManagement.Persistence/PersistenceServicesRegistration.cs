using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Persistence.Repositories;

namespace TodoManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoManagementDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TodoManagementConnectionString"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
