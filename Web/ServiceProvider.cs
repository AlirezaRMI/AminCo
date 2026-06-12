using System.Text;
using Application.Logics.Intefaces;
using Domain.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["accessToken"];
                            if (!string.IsNullOrEmpty(token))
                                context.Token = token;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("JWT Authentication Failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });

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