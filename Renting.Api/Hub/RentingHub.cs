using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Renting.Api.Hub
{
    public class RentingHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task EnviarRespuestaFacturaCreada(object respuesta)
        {
            await Clients.All.SendAsync("RespuestaFacturaCreada", respuesta);
        }
    }
}
