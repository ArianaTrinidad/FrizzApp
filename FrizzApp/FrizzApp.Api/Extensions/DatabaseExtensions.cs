using FrizzApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FrizzApp.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var useSqlLite = configuration.GetValue<bool>("UseSqlLite");

            if (useSqlLite)
            {
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<DataContext>(option =>
                        option.UseSqlite("Filename=FrizzAppDB.sqlite;"));
            }
            else
            {
                string connectionString = Environment.GetEnvironmentVariable("FrizzAppDB");

                services
                    .AddDbContext<DataContext>(option =>
                        option.UseSqlServer(configuration.GetConnectionString("FrizzAppDB")));
            }

            return services;
        }
    }
}
