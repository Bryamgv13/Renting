using Renting.Domain.Ports;
using System.Threading.Tasks;

namespace Renting.Worker.Tests
{
    public class RepositorioHubTest : IRepositorioHub
    {
        public Task InvocarHubAsync(string nombreMetodo, object mensaje)
        {
            return Task.CompletedTask;
        }
    }
}
