using Renting.Domain.DomainException;
using Renting.Domain.Entities;
using Renting.Domain.Enum;
using Renting.Domain.Ports;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Renting.Domain.Services
{
    [DomainService]
    public class ServicioValidaPicoYPlacaVehiculo
    {
        private readonly IGenericRepository<PicoYPlaca> RepositorioPicoYPlaca;

        public ServicioValidaPicoYPlacaVehiculo(IGenericRepository<PicoYPlaca> repositorioPicoYPlaca)
        {
            RepositorioPicoYPlaca = repositorioPicoYPlaca;
        }

        public async Task ValidarIngresoVehiculoAsync(string placa, TipoVehiculo tipo)
        {
            var picoYPlacas = await RepositorioPicoYPlaca.GetAsync(picoYPlaca => picoYPlaca.Dia.Equals(((int)DateTime.Now.DayOfWeek))
                                    && picoYPlaca.Tipo.Equals(tipo));
            if (picoYPlacas.Any())
            {
                PicoYPlaca picoYPlaca = picoYPlacas.FirstOrDefault();
                if (picoYPlaca.ObtenerPlacas().Contains(ExtraerDigitoPlaca(placa, tipo)))
                {
                    throw new VehiculoEnPicoYPlacaException("El vehiculo se encuentra en pico y placa");
                }
            }            
        }

        private string ExtraerDigitoPlaca(string placa, TipoVehiculo tipo)
        {
            if (tipo.Equals(TipoVehiculo.Carro))
            {
                return placa.Substring(5);
            } else
            {
                return placa.Substring(3, 1);
            }
        }
    }
}
