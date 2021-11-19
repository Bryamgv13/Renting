using MediatR;
using Renting.Application.Ports;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class FacturaVehiculoFromMessageHandler : AsyncRequestHandler<FacturaVehiculoFromMessage>
    {
        private readonly IAlmacenamiento Almacenamiento;

        public FacturaVehiculoFromMessageHandler(IAlmacenamiento almacenamiento)
        {
            Almacenamiento = almacenamiento;
        }

        protected override async Task Handle(FacturaVehiculoFromMessage request, CancellationToken cancellationToken)
        {
            await Almacenamiento.SubirArchivoBlobAsync(request.Placa, new MemoryStream(Encoding.Unicode.GetBytes(request.Valor.ToString())));
        }
    }
}
