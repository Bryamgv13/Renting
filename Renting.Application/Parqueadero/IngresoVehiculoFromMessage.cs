using MediatR;
using Renting.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.Parqueadero
{
    public class IngresoVehiculoFromMessage : IRequest
    {
        public IngresoVehiculoFromMessage(string placa, TipoVehiculo tipo, int cilindraje)
        {
            Placa = placa;
            Tipo = tipo;
            Cilindraje = cilindraje;
        }

        [Required]
        public string Placa { get; set; }
        [Required]
        public TipoVehiculo Tipo { get; set; }
        public int Cilindraje { get; set; }
    }
}
