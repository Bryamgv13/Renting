using MediatR;
using Renting.Application.Ports;
using Renting.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class IngresoVehiculoCommandHandler : AsyncRequestHandler<IngresoVehiculoCommand>
    {
        private readonly ServicioValidaPicoYPlacaVehiculo ServicioValidaPicoYPlacaVehiculo;
        private readonly ServicioValidarEspacioParqueadero ServicioValidarEspacioParqueadero;
        private readonly IBusMessaging BusMessaging;

        public IngresoVehiculoCommandHandler(ServicioValidaPicoYPlacaVehiculo servicioValidaPicoYPlacaVehiculo,
            ServicioValidarEspacioParqueadero servicioValidarEspacioParqueadero,
            IBusMessaging busMessaging)
        {
            ServicioValidaPicoYPlacaVehiculo = servicioValidaPicoYPlacaVehiculo;
            ServicioValidarEspacioParqueadero = servicioValidarEspacioParqueadero;
            BusMessaging = busMessaging;
        }

        protected override async Task Handle(IngresoVehiculoCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request object needed to handle this task");
            await ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(request.Placa, request.Tipo);
            await ServicioValidarEspacioParqueadero.ValidarIngresoVehiculoAsync(request.Tipo);
            await BusMessaging.SendMessageAsync(request, "ingreso-vehiculo");
        }
    }
}
