using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Renting.Api
{
    public class Program
    {
        protected Program()
        {

        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostBuilderContext, loggerConfig) =>
                {
                    loggerConfig.MinimumLevel.Information()
                        .ReadFrom.Configuration(hostBuilderContext.Configuration)
                        .WriteTo.Console()
                        .WriteTo.File($"Renting.Api-{DateTime.Now.Millisecond}.log", rollingInterval: RollingInterval.Day);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
