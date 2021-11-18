using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Renting.Api.Extensions;
using Renting.Api.Filters;
using Renting.Application.Ports;
using Renting.Infrastructure.Adapters;
using Renting.Infrastructure.Extensions;
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

            services.AddServiceBusSupport(Configuration);
            services.AddTransient<IBusMessaging, MessagingAdapter>();

            services.AddControllers(mvcOpts =>
            {
                mvcOpts.Filters.Add(typeof(AppExceptionFilterAttribute));
            });

            services.AddSingleton(cfg => Configuration);
            services.AddHealthChecks()
                .AddDbContextCheck<Infrastructure.PersistenceContext>();

            services.LoadAppStoreRepositories();
            services.AddSwaggerDocument();

        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.SeedDataBase();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
