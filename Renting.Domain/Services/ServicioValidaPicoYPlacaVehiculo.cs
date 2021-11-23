using Renting.Domain.DomainException;
using Renting.Domain.Entities;
using Renting.Domain.Enum;
using Renting.Domain.Ports;
using System;
using System.Linq;

namespace Renting.Domain.Services
{
    [DomainService]
    public class ServicioValidaPicoYPlacaVehiculo
    {
        private readonly IRepositorioTable RepositorioPicoYPlaca;
        private readonly IProveedorMensajes ProveedorMensajes;

        public ServicioValidaPicoYPlacaVehiculo(IRepositorioTable repositorioPicoYPlaca,
            IProveedorMensajes proveedorMensajes)
        {
            RepositorioPicoYPlaca = repositorioPicoYPlaca;
            ProveedorMensajes = proveedorMensajes;
        }

        public void ValidarIngresoVehiculoAsync(string placa, TipoVehiculo tipo)
        {
            var picoYPlacas = RepositorioPicoYPlaca.ObtenerPicoYPlacaPorDiaYTipo((int)DateTime.Now.DayOfWeek, (int)tipo);
                
            if (picoYPlacas.Any())
            {
                PicoYPlaca picoYPlaca = picoYPlacas.FirstOrDefault();
                if (picoYPlaca.ObtenerPlacas().Contains(ExtraerDigitoPlaca(placa, tipo)))
                {
                    throw new VehiculoEnPicoYPlacaException(ProveedorMensajes.PicoYPlaca);
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
