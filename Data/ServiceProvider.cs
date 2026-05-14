using Data.Context;
using Data.Interceptors;
using Data.Ripositores;
using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();
            
            services.AddDbContext<AmincoDbContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

                options.UseSqlServer(connectionString);
                
                var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();
                options.AddInterceptors(auditInterceptor);
            });

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));

            return services;
        }
    }
}