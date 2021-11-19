using AutoMapper;
using MediatR;
using Renting.Application.Ports;
using Renting.Domain.DomainException;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class IngresoVehiculoCommandHandler : AsyncRequestHandler<IngresoVehiculoCommand>
    {
        private readonly ServicioValidaPicoYPlacaVehiculo ServicioValidaPicoYPlacaVehiculo;      
        private readonly IMapper Mapper;
        private readonly IGenericRepository<Domain.Entities.Parqueadero> RepositorioParqueadero;
        private readonly IGenericRepository<Domain.Entities.Vehiculo> RepositorioVehiculo;

        public IngresoVehiculoCommandHandler(ServicioValidaPicoYPlacaVehiculo servicioValidaPicoYPlacaVehiculo,
            IMapper mapper,
            IGenericRepository<Domain.Entities.Parqueadero> repositorioParqueadero,
            IGenericRepository<Domain.Entities.Vehiculo> repositorioVehiculo)
        {
            ServicioValidaPicoYPlacaVehiculo = servicioValidaPicoYPlacaVehiculo;
            Mapper = mapper;
            RepositorioParqueadero = repositorioParqueadero;
            RepositorioVehiculo = repositorioVehiculo;
        }

        protected override async Task Handle(IngresoVehiculoCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request object needed to handle this task");
            ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(request.Placa, request.Tipo);
            Domain.Entities.Parqueadero parqueadero = (await RepositorioParqueadero.GetAsync(includeStringProperties: "Vehiculos")).FirstOrDefault();
            if (parqueadero.EstaLleno(request.Tipo))
            {
                throw new ParqueaderoLlenoException("No hay espacio para el vehiculo");
            }
            Domain.Entities.Vehiculo vehiculo = Mapper.Map<Domain.Entities.Vehiculo>(request);
            vehiculo.Ingreso = DateTime.Now;
            vehiculo.ParqueaderoId = parqueadero.Id;
            await RepositorioVehiculo.AddAsync(vehiculo);
        }
    }
}
