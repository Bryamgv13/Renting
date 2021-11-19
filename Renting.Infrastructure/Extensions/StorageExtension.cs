using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Renting.Infrastructure.Extensions
{
    public static class StorageExtension
    {
        public static IServiceCollection AddStorageSupport(this IServiceCollection services, IConfiguration config)
        {
            var client = new BlobContainerClient(Environment.GetEnvironmentVariable("STORAGE_CONNSTRING") ?? config.GetValue<string>("STORAGE:CONNSTRING"),
                config.GetValue<string>("STORAGE:CONTAINER_BLOB"));
            services.AddSingleton<BlobContainerClient>(client);
            return services;
        }
    }
}
