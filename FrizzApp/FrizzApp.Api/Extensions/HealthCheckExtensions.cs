using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;

namespace FrizzApp.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddSqlServer(
                    connectionString: configuration["ConnectionStrings:FrizzAppDB"],
                    name: "SQL Databse",
                    tags: new List<string>(1) { "sql", "databases" },
                    failureStatus: HealthStatus.Unhealthy
                )
                .AddRedis(
                    redisConnectionString: LibrariesExtensions.GetRedisConnectionString(configuration),
                    name: "Redis instance",
                    tags: new List<string>(1) { "redis" },
                    failureStatus: HealthStatus.Unhealthy
                )
                .AddUrlGroup(
                    uri: new Uri("https://dollar-conversor.herokuapp.com/cotizacion/usd-ar/actual"),
                    name: "Dollar Exchange Api",
                    tags: new List<string>(1) { "external url", "3rd services" },
                    failureStatus: HealthStatus.Degraded
                );

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
                opt.AddHealthCheckEndpoint("Frizzapp Api", "/health");
            })
            .AddInMemoryStorage();

            return services;
        }
    }
}
