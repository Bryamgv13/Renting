using Renting.Domain.DomainException;
using Renting.Domain.Entities;
using Renting.Domain.Enum;
using Renting.Domain.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace Renting.Domain.Services
{
    [DomainService]
    public class ServicioValidarEspacioParqueadero
    {
        private readonly int CELDAS_CARRO = 20;
        private readonly int CELDAS_MOTO = 10;

        private readonly IGenericRepository<Parqueadero> RepositorioParqueadero;

        public ServicioValidarEspacioParqueadero(IGenericRepository<Parqueadero> repositorioParqueadero)
        {
            RepositorioParqueadero = repositorioParqueadero;
        }

        public async Task ValidarIngresoVehiculoAsync(TipoVehiculo tipo)
        {
            var vehiculos = (await RepositorioParqueadero.GetAsync(vehiculo => vehiculo.Tipo.Equals(tipo)
                                    && vehiculo.Salida == null)).Count();
            if ((tipo == TipoVehiculo.Carro && vehiculos >= CELDAS_CARRO)
                || (tipo == TipoVehiculo.Moto && vehiculos >= CELDAS_MOTO))
            {
                throw new ParqueaderoLlenoException("No hay espacio para el vehiculo");
            }
        }
    }
}
