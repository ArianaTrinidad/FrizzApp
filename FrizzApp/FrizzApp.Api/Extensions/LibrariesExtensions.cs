using FirzzApp.Business.Mappings;
using FirzzApp.Business.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using System;
using FirzzApp.Business.Interfaces;

namespace FrizzApp.Api.Extensions
{
    public static class LibrariesExtensions
    {
        public static IServiceCollection AddLibraries(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(CategoryMapping).Assembly);
            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());
            var url = configuration.GetValue<string>("RedisConfiguration:Url");
            var port = configuration.GetValue<int>("RedisConfiguration:Port");
            var password = configuration.GetValue<string>("RedisConfiguration:Password");
            var redisConnectionString = $"{url}:{port},password={password}";
            services.AddHealthChecks()
                    .AddSqlServer(configuration["ConnectionStrings:FrizzAppDB"])
                    .AddRedis(redisConnectionString)
                    .AddUrlGroup(new Uri("https://dollar-conversor.herokuapp.com/cotizacion/usd-ar/actual"));
            return services;
        }
    }
}
