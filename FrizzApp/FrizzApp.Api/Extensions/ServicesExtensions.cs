using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FrizzApp.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IProductStatusService, ProductStatusService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICacheService, CacheService>();

            return services;
        }
    }
}
