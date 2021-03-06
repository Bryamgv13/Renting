using AutoMapper;
using Azure.Data.Tables;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using Renting.Infrastructure;
using Renting.Infrastructure.Adapters;
using Renting.Infrastructure.Extensions;
using Renting.Infrastructure.Recursos;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Renting.Worker
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
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapHealthChecks("/health", new HealthCheckOptions
                            {
                                AllowCachingResponses = false
                            });
                        });
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var executingAssembly = typeof(Program).Assembly;
                    services.AddMediatR(Assembly.Load("Renting.Application"), executingAssembly);
                    var applicationAssembly = executingAssembly.GetReferencedAssemblies()
                        .FirstOrDefault(x => x.Name.Equals("Renting.Application", System.StringComparison.InvariantCulture));

                    services.AddAutoMapper(Assembly.Load(applicationAssembly.FullName));
                    var JsonConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", false).Build();

                    services.AddDbContext<PersistenceContext>(opt =>
                    {
                        opt.UseInMemoryDatabase("database");
                    });

                    services.AddHealthChecks()
                        .AddDbContextCheck<PersistenceContext>();

                    services.AddSingleton<TableClient>(new TableClient(Environment.GetEnvironmentVariable("STORAGE_CONNSTRING") ?? JsonConfig.GetValue<string>("STORAGE:CONNSTRING"), "PicoYPlaca"));

                    string urlSignalR = Environment.GetEnvironmentVariable("URL_SIGNALR_HUB") ?? JsonConfig.GetValue<string>("HUB:Url");
                    HubConnection hub = new HubConnectionBuilder()
                                            .WithUrl(urlSignalR, options =>
                                            {
                                                options.SkipNegotiation = true;
                                                options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                                            })
                                            .Build();
                    services.AddScoped<HubConnection>(c => hub);

                    services.AddSingleton(c => JsonConfig);
                    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                    services.AddTransient<IBusMessaging, MessagingAdapter>();
                    services.AddTransient<IAlmacenamiento, AzureStorage>();
                    services.AddTransient<IRepositorioHub, RepositorioHub>();
                    services.AddTransient<IRepositorioTable, RepositorioTable>();
                    services.AddTransient<IProveedorConstantes, ProveedorConstantes>();
                    services.AddTransient<IProveedorMensajes, ProveedorMensajes>();
                    services.AddTransient<ServicioValidaPicoYPlacaVehiculo>();
                    services.AddTransient<ServicioCalcularValorAPagar>();
                    services.AddHostedService<Worker>();
                    services.AddServiceBusSupport(JsonConfig);
                    services.AddStorageSupport(JsonConfig);
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .WriteTo.File($"{typeof(Program).Assembly.GetName().Name}.log", rollingInterval: RollingInterval.Day)
                        .WriteTo.Console()
                        .CreateLogger();
                });
    }
}


