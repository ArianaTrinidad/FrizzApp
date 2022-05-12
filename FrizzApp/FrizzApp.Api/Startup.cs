using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Mappings;
using FirzzApp.Business.Services;
using FirzzApp.Business.Validators.ProductValidators;
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
using Microsoft.OpenApi.Models;

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


            services.AddMemoryCache();

            services.AddAutoMapper(typeof(CategoryMapping).Assembly);

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IProductStatusService, ProductStatusService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IPaymentTypeRepository, PymentTypeRepository>();
            services.AddTransient<IProductStatusRepository, ProductStatusRepository>();

            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());

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
