using Renting.Domain.Ports;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Renting.Infrastructure.Adapters
{
    public class RepositorioHub : IRepositorioHub
    {
        private readonly HubConnection ClienteHub;

        public RepositorioHub(HubConnection clienteHub)
        {
            ClienteHub = clienteHub;
        }

        public async Task InvocarHubAsync(string nombreMetodo, object mensaje)
        {
            if (ClienteHub.State == HubConnectionState.Disconnected)
            {
                await ClienteHub.StartAsync();
            }
            await ClienteHub.InvokeAsync(nombreMetodo, mensaje).ConfigureAwait(false);
        }
    }
}
