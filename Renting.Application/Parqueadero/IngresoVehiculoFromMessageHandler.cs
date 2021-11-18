using AutoMapper;
using MediatR;
using Renting.Domain.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Renting.Application.Parqueadero
{
    public class IngresoVehiculoFromMessageHandler : AsyncRequestHandler<IngresoVehiculoFromMessage>
    {
        private readonly IMapper Mapper;
        private readonly IGenericRepository<Domain.Entities.Parqueadero> RepositorioParqueadero;

        public IngresoVehiculoFromMessageHandler(IMapper mapper,
            IGenericRepository<Domain.Entities.Parqueadero> repositorioParqueadero)
        {
            Mapper = mapper;
            RepositorioParqueadero = repositorioParqueadero;
        }

        protected override async Task Handle(IngresoVehiculoFromMessage request, CancellationToken cancellationToken)
        {
            Domain.Entities.Parqueadero parqueadero = Mapper.Map<Domain.Entities.Parqueadero>(request);
            await RepositorioParqueadero.AddAsync(parqueadero);
        }
    }
}
