using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Services;
using FirzzApp.Business.Validators;
using FluentValidation.AspNetCore;
using FrizzApp.Api.Middlewares;
using FrizzApp.Data;
using FrizzApp.Data.Interfaces;
using FrizzApp.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<DataContext>(option =>
                    option.UseSqlServer(Configuration.GetConnectionString("FrizzAppDB")));

            //.AddDbContext<DataContext>(option =>
            //    option.UseSqlite("Filename=FrizzAppDB.sqlite;"));


            var pepe1 = Configuration.GetSection("PepeString"); ; // Obtener el dato "pepe-1" de "PepeString"
            var pepe2 = Configuration.GetSection("PepeObject:Pepevalue").Value; // Obtener el dato "pepe-2" del objecto "PepeObject" en su propiedad "Pepevalue"
            var pepe3 = Configuration.GetSection("PepeArray")
                                     .GetChildren()
                                     .ToList().Select(x => (x.GetValue<string>("PepeArr1")));// Obtener el dato "pepe-3" del array "PepeArray", su primer item
            var pepe4 = Configuration.GetSection("PepeArray")
                                     .GetChildren()
                                     .ToList().Select(x => (x.GetValue<string>("PepeArr2"))); // Obtener el dato "pepe-4" del array "PepeArray", su segundo item


            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FrizzApp.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }

            //Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            //if (env.IsDevelopment())
            //{
            //    Console.WriteLine("hello dev");
            //}

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrizzApp.Api v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
