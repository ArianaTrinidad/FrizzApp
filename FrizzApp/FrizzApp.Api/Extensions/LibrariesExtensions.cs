using FirzzApp.Business.Mappings;
using FirzzApp.Business.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddHealthChecks()
                    .AddSqlServer(configuration["ConnectionStrings:FrizzAppDB"]);

            return services;
        }
    }
}
