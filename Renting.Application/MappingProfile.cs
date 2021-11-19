using AutoMapper;
using Renting.Application.Parqueadero;

namespace Renting.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IngresoVehiculoCommand, Domain.Entities.Vehiculo>();
        }
    }
}
