using MediatR;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class FacturaVehiculoFromMessageHandler : AsyncRequestHandler<FacturaVehiculoFromMessage>
    {
        private readonly IAlmacenamiento Almacenamiento;
        private readonly IRepositorioHub RepositorioHub;

        public FacturaVehiculoFromMessageHandler(IAlmacenamiento almacenamiento, IRepositorioHub repositorioHub)
        {
            Almacenamiento = almacenamiento;
            RepositorioHub = repositorioHub;
        }

        protected override async Task Handle(FacturaVehiculoFromMessage request, CancellationToken cancellationToken)
        {
            await Almacenamiento.SubirArchivoBlobAsync(request.Placa, new MemoryStream(Encoding.Unicode.GetBytes(request.Valor.ToString())));
            await RepositorioHub.InvocarHubAsync("EnviarRespuestaFacturaCreada",  $"Factura por valor {request.Valor} de creada");

        }
    }
}
