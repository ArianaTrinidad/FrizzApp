using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrizzApp.Api.Extensions
{
    public static class LogExtensions
    {
        public static IServiceCollection AddLog(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}
