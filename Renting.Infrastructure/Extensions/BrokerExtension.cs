using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Renting.Infrastructure.Extensions
{
    public static class BrokerExtension
    {
        public static IServiceCollection AddServiceBusSupport(this IServiceCollection services, IConfiguration config)
        {
            var client = new ServiceBusClient(Environment.GetEnvironmentVariable("SERVICEBUS_CONNSTRING") ?? config.GetValue<string>("SERVICEBUS:CONNSTRING"));
            services.AddSingleton<ServiceBusClient>(client);
            return services;
        }

    }
}
