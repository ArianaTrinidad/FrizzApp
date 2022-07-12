using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrizzApp.Api.Extensions
{
    public static class SeviciosExtensions
    {
        public static void AddServicios(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IProductStatusService, ProductStatusService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICacheService, CacheService>();
        }
    }
}
