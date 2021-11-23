using AutoMapper;
using Azure.Data.Tables;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Renting.Api.Extensions;
using Renting.Api.Filters;
using Renting.Api.Hub;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using Renting.Infrastructure.Adapters;
using Renting.Infrastructure.Extensions;
using Renting.Infrastructure.Recursos;
using Renting.Infrastructure.Seed;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Renting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMediatR(Assembly.Load("Renting.Application"), typeof(Startup).Assembly);

            var applicationAssemblyName = typeof(Startup).Assembly.GetReferencedAssemblies()
                .FirstOrDefault(x => x.Name.Equals("Renting.Application", StringComparison.InvariantCulture));

            services.AddAutoMapper(Assembly.Load(applicationAssemblyName.FullName));

            services.AddDbContext<Infrastructure.PersistenceContext>(opt =>
            {
                opt.UseInMemoryDatabase("database");
            });

            services.AddSignalR();

            string urlSignalR = Environment.GetEnvironmentVariable("URL_SIGNALR_HUB") ?? Configuration.GetValue<string>("HUB:Url");
            HubConnection hub = new HubConnectionBuilder()
                                    .WithUrl(urlSignalR, options =>
                                    {
                                        options.SkipNegotiation = true;
                                        options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                                    })
                                    .Build();
            services.AddScoped<HubConnection>(c => hub);

            services.AddServiceBusSupport(Configuration);
            services.AddStorageSupport(Configuration);
            services.AddTransient<IBusMessaging, MessagingAdapter>();
            services.AddTransient<IAlmacenamiento, AzureStorage>();
            services.AddTransient<IRepositorioTable, RepositorioTable>();
            services.AddTransient<IRepositorioHub, RepositorioHub>();
            services.AddTransient<IProveedorConstantes, ProveedorConstantes>();
            services.AddTransient<IProveedorMensajes, ProveedorMensajes>();

            services.AddControllers(mvcOpts =>
            {
                mvcOpts.Filters.Add(typeof(AppExceptionFilterAttribute));
            });

            services.AddSingleton(cfg => Configuration);
            services.AddHealthChecks()
                .AddDbContextCheck<Infrastructure.PersistenceContext>();

            services.LoadAppStoreRepositories();
            services.AddSwaggerDocument();

            services.AddSingleton<TableClient>(new TableClient(Environment.GetEnvironmentVariable("STORAGE_CONNSTRING") ?? Configuration.GetValue<string>("STORAGE:CONNSTRING"), "PicoYPlaca"));

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
            }));
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.SeedDataBase();
            app.SeedTableStorage();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<RentingHub>("/Hub");
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
