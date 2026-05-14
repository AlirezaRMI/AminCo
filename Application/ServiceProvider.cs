using System.Reflection;
using Application.Logics.Intefaces;
using Application.Logics.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceProvider
    {
        /// <summary>
        /// Registers all application services (business logic), AutoMapper,
        /// and any required configuration settings.
        /// </summary>
        public static IServiceCollection ApplicationServiceProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            services.AddScoped<ISpecialSaleService, SpecialSaleService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICustomDesignRequestService, CustomDesignRequestService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IContactInfoService, ContactInfoService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IPortfolioImageService, PortfolioImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<INotificationService, NotificationService>();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // services.Configure<FileStorageSettings>(
            //     configuration.GetSection("FileStorageSettings"));
            // services.AddOptionsWithValidateOnStart<FileStorageSettings>();

            return services;
        }
    }
}