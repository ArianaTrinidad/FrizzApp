using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
