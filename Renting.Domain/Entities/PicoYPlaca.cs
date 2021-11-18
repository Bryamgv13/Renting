using Renting.Domain.Enum;
using System;

namespace Renting.Domain.Entities
{
    public class PicoYPlaca : EntityBase<Guid>
    {
        public int Dia { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public string Placas { get; set; }

        public string[] ObtenerPlacas()
        {
            return this.Placas.Split('-');
        }
    }
}
