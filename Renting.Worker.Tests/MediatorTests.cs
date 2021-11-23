using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renting.Application.Parqueadero;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using Renting.Infrastructure;
using Renting.Infrastructure.Adapters;
using Renting.Infrastructure.Extensions;
using Renting.Worker.Tests;
using Serilog;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Worker.Test
{
    [TestClass]
    public class MediatorTests
    {
        IServiceCollection _services;

        [TestInitialize]
        public void Initialize()
        {
            _services = new ServiceCollection();
            var executingAssembly = typeof(MediatorTests).Assembly;
            _services.AddMediatR(Assembly.Load("Renting.Application"), executingAssembly);
            var applicationAssembly = executingAssembly.GetReferencedAssemblies()
                .FirstOrDefault(x => x.Name.Equals("Renting.Application", System.StringComparison.InvariantCulture));

            _services.AddAutoMapper(Assembly.Load(applicationAssembly.FullName));
            var JsonConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false).Build();

            _services.AddDbContext<PersistenceContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            HubConnection hub = new HubConnectionBuilder()
                                    .WithUrl(JsonConfig.GetValue<string>("HUB:Url"))
                                    .Build();
            _services.AddScoped<HubConnection>(c => hub);

            _services.AddSingleton<IConfiguration>(c => JsonConfig);
            _services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            _services.AddTransient<IBusMessaging, MessagingAdapter>();
            _services.AddTransient<IAlmacenamiento, AzureStorage>();
            _services.AddTransient<IRepositorioHub, RepositorioHubTest>();
            _services.AddTransient<IRepositorioTable, RepositorioTable>();
            _services.AddTransient<ServicioValidaPicoYPlacaVehiculo>();
            _services.AddTransient<ServicioCalcularValorAPagar>();
            _services.AddServiceBusSupport(JsonConfig);
            _services.AddStorageSupport(JsonConfig);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .CreateLogger();
        }


        [TestMethod]
        public void FacturaVehiculo()
        {
            var newPerson = new FacturaVehiculoFromMessage("AAA571", Renting.Domain.Enum.TipoVehiculo.Carro, 1500, 8000);

            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                var result = mediator.Send(newPerson).Result;
                Assert.IsInstanceOfType(result, typeof(Unit));
            }
        }
    }
}
