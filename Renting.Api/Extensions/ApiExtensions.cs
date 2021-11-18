using Microsoft.Extensions.DependencyInjection;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using Renting.Infrastructure.Adapters;
using System;
using System.Linq;

namespace Renting.Api.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection LoadAppStoreRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var _services = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.Contains("Domain", StringComparison.InvariantCulture))
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            foreach (var _service in _services)
            {
                // interfaces in Domain layer, ignoring interfaces in other namespaces
                var sInterf = _service.GetInterfaces().FirstOrDefault(i => i.FullName.Contains("Domain", StringComparison.InvariantCulture));
                if (sInterf != null)
                {
                    services.AddTransient(sInterf, _service);
                } else
                {
                    services.AddTransient(_service);
                }
            }
            return services;
        }
    }
}
