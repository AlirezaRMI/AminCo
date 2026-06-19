using Application.Logics.Intefaces;
using Domain.Contract;
using Web.Filters;
using Web.Securities;
using Web.Services;

namespace Web
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
                throw new Exception("JwtSettings not configured properly");

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            
            services.AddAuthorization();

            services.AddOpenApi("docs", options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });
            services.AddEndpointsApiExplorer();

            services.AddHttpContextAccessor();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<AdminAuthFilter>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddSingleton<IJwtService, JwtService>();

            return services;
        }
    }
}