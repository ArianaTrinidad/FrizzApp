using FirzzApp.Business.Auth;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Mappings;
using FirzzApp.Business.Services;
using FirzzApp.Business.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using FrizzApp.Api.Middlewares;
using FrizzApp.Data;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using FrizzApp.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;

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

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });



            var useSqlLite = Configuration.GetValue<bool>("UseSqlLite");

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
                        option.UseSqlServer(Configuration.GetConnectionString("FrizzAppDB")));
            }

            services.AddHttpContextAccessor();

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(CategoryMapping).Assembly);

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IProductStatusService, ProductStatusService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IPaymentTypeRepository, PymentTypeRepository>();
            services.AddTransient<IProductStatusRepository, ProductStatusRepository>();

            services.AddTransient<ICacheService, CacheService>();

            services.AddSingleton(Log.Logger);

            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DataContext>();

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FrizzApp.Api", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrizzApp.Api v1"));
        }
    }
}
