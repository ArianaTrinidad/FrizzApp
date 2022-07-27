using FirzzApp.Business.Mappings;
using FirzzApp.Business.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using FrizzApp.Api.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace FrizzApp.Api.Extensions
{
    public static class LibrariesExtensions
    {
        public static IServiceCollection AddLibraries(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(CategoryMapping).Assembly);
            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());
            services.AddHealthChecks()
                    .AddCheck<DataBaseHealthCheck>("Data Base health check is running");

            return services;
        }
    }
}
