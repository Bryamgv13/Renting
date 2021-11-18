using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Renting.Application.Parqueadero;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Worker
{
    public sealed class Worker : BackgroundService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _config;

        ServiceBusProcessor processor;

        public Worker(IServiceProvider services, IEnumerable<IConnection> brokerConnection)
        {
            _services = services ?? throw new ArgumentNullException("services", "Service provider required to build scopes");
            _logger = _services.GetService(typeof(ILogger<Worker>)) as ILogger<Worker>;
            _config = _services.GetService(typeof(IConfiguration)) as IConfiguration;
            _serviceBusClient = _services.GetService(typeof(ServiceBusClient)) as ServiceBusClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueName = _config.GetValue<string>("SERVICEBUS:QUEUE");

            processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += async (args) =>
                {
                    try
                    {
                        using (var scope = _services.CreateScope())
                        {
                            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            var ingresarVehiculoRequest = System.Text.Json.JsonSerializer.Deserialize<IngresoVehiculoCommand>(System.Text.Encoding.Default.GetString(args.Message.Body.ToArray()));
                            _ = await _mediator.Send(new IngresoVehiculoFromMessage(ingresarVehiculoRequest.Placa,
                                ingresarVehiculoRequest.Tipo, ingresarVehiculoRequest.Cilindraje));
                            await args.CompleteMessageAsync(args.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"some error : {ex.Message}");
                    }
                };

                processor.ProcessErrorAsync += (args) =>
                {
                    _logger.LogError(args.Exception, $"some error : {args.Exception.Message}");
                    return Task.CompletedTask;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"some error : {ex.Message}");
            }
            await processor.StartProcessingAsync();
            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            processor.DisposeAsync();
            base.Dispose();
        }

    }
}
