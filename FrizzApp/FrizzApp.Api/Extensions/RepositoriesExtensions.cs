using FrizzApp.Data.Interfaces;
using FrizzApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrizzApp.Api.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IPaymentTypeRepository, PymentTypeRepository>();
            services.AddTransient<IProductStatusRepository, ProductStatusRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}
