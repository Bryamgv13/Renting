using MediatR;
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
        private readonly IGenericRepository<Domain.Entities.Parqueadero> RepositorioParqueadero;
        private readonly ServicioCalcularValorAPagar ServicioCalcularValorAPagar;

        public SalidaVehiculoCommandHandler(IGenericRepository<Domain.Entities.Parqueadero> repositorioParqueadero,
            ServicioCalcularValorAPagar servicioCalcularValorAPagar)
        {
            RepositorioParqueadero = repositorioParqueadero;
            ServicioCalcularValorAPagar = servicioCalcularValorAPagar;
        }

        public async Task<InformacionSalidaDto> Handle(SalidaVehiculoCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request object needed to handle this task");
            var vehiculo = (await RepositorioParqueadero.GetAsync(vehiculo => vehiculo.Placa.Equals(request.Placa))).FirstOrDefault();
            var valor = ServicioCalcularValorAPagar.CalcularValor(vehiculo);
            return new InformacionSalidaDto { Valor = valor };
        }
    }
}
