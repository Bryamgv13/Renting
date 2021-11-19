using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using Renting.Infrastructure;
using Renting.Infrastructure.Adapters;
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

            _services.AddSingleton<IConfiguration>(c => JsonConfig);
            _services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //_services.AddTransient<IPersonService, PersonService>();
            _services.AddTransient<IBusMessaging, MessagingAdapter>();
            //_services.AddRabbitSupport(JsonConfig);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .CreateLogger();
        }


        [TestMethod]
        public void TestMethod1()
        {

            //var newPerson = new CreatePersonFromMessage("john", "doe", "john@doe.com");

            //using (var scope = _services.BuildServiceProvider().CreateScope())
            //{
            //    var mediator = scope.ServiceProvider.GetService<IMediator>();
            //    var result = mediator.Send(newPerson).Result;
            //    Assert.IsInstanceOfType(result, typeof(Unit));
            //}
        }
    }
}
