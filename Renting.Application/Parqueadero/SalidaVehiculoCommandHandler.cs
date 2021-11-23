using MediatR;
using Renting.Application.Ports;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class SalidaVehiculoCommandHandler : IRequestHandler<SalidaVehiculoCommand, InformacionSalidaDto>
    {
        private readonly IGenericRepository<Domain.Entities.Vehiculo> RepositorioVehiculo;
        private readonly ServicioCalcularValorAPagar ServicioCalcularValorAPagar;
        private readonly IBusMessaging BusMessaging;

        public SalidaVehiculoCommandHandler(IGenericRepository<Domain.Entities.Vehiculo> repositorioVehiculo,
            ServicioCalcularValorAPagar servicioCalcularValorAPagar,
            IBusMessaging busMessaging)
        {
            RepositorioVehiculo = repositorioVehiculo;
            ServicioCalcularValorAPagar = servicioCalcularValorAPagar;
            BusMessaging = busMessaging;
        }

        public async Task<InformacionSalidaDto> Handle(SalidaVehiculoCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request object needed to handle this task");
            var vehiculo = (await RepositorioVehiculo.GetAsync(vehiculo => vehiculo.Placa.Equals(request.Placa) && vehiculo.Valor == 0)).FirstOrDefault();
            var valor = ServicioCalcularValorAPagar.CalcularValor(vehiculo);
            vehiculo.Salida = DateTime.Now;
            vehiculo.Valor = valor;
            await RepositorioVehiculo.UpdateAsync(vehiculo);
            await BusMessaging.SendMessageAsync(vehiculo, "ingreso-vehiculo");
            return new InformacionSalidaDto { Valor = valor };
        }
    }
}
