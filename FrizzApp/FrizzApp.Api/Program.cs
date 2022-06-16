using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FrizzApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureSerilog();
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog();
                });


        private static void ConfigureSerilog()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .AddJsonFile(
                                "appsettings.json",
                                optional: false,
                                reloadOnChange: true).Build();
            
            System.Console.WriteLine(configuration.GetValue<string>("CreateProductKey")); 
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
