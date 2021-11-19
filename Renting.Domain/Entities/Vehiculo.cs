using Renting.Domain.Enum;
using System;

namespace Renting.Domain.Entities
{
    public class Vehiculo : EntityBase<Guid>
    {
        public string Placa { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public int Cilindraje { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime Salida { get; set; }
        public double Valor { get; set; }

        public Parqueadero Parqueadero { get; set; }
        public Guid ParqueaderoId { get; set; }
    }
}
