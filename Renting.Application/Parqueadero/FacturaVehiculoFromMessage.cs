using MediatR;
using Renting.Domain.Enum;

namespace Renting.Application.Parqueadero
{
    public class FacturaVehiculoFromMessage : IRequest
    {
        public FacturaVehiculoFromMessage(string placa, TipoVehiculo tipo, int cilindraje, double valor)
        {
            Placa = placa;
            Tipo = tipo;
            Cilindraje = cilindraje;
            Valor = valor;
        }

        public string Placa { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public int Cilindraje { get; set; }
        public double Valor { get; set; }
    }
}
