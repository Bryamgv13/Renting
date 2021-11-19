using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Renting.Api.Hub
{
    public class RentingHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task EnviarRespuestaFacturaCreada(string idConexion, object respuesta)
        {
            await Clients.Client(idConexion).SendAsync("RespuestaFacturaCreada", respuesta);
        }
    }
}
