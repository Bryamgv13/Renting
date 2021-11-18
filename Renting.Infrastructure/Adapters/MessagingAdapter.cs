using Azure.Messaging.ServiceBus;
using Renting.Application.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Renting.Infrastructure.Adapters
{
    public class MessagingAdapter : IBusMessaging
    {
        private readonly ServiceBusClient ServiceBusClient;

        public MessagingAdapter(ServiceBusClient serviceBusClient)
        {
            ServiceBusClient = serviceBusClient;
        }
        
        public async Task SendMessageAsync(object o, string queue)
        {
            ServiceBusSender sender = ServiceBusClient.CreateSender(queue);

            var message = System.Text.Json.JsonSerializer.Serialize(o);
            var body = Encoding.UTF8.GetBytes(message);

            ServiceBusMessage messageBus = new ServiceBusMessage(body);

            try
            {
                await sender.SendMessageAsync(messageBus);
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }
    }
}
