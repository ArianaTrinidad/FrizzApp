using FirzzApp.Business.Mappings;
using FirzzApp.Business.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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

            return services;
        }


        public static string GetRedisConnectionString(IConfiguration configuration)
        {
            var redisUrl = configuration.GetValue<string>("RedisConfiguration:Url");
            var redisPort = configuration.GetValue<int>("RedisConfiguration:Port");
            var redisPassword = configuration.GetValue<string>("RedisConfiguration:Password");
            var redisConnectionString = $"{redisUrl}:{redisPort},password={redisPassword}";
            return redisConnectionString;
        }
    }
}
