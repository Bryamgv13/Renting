using Renting.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renting.Domain.Entities
{
    public class Parqueadero : EntityBase<Guid>
    {
        public int CeldasCarro { get; set; }
        public int CeldasMoto { get; set; }
        public IEnumerable<Vehiculo> Vehiculos { get; set; }

        public bool EstaLleno(TipoVehiculo tipo)
        {
            if ((tipo == TipoVehiculo.Carro && Vehiculos.Count(v => v.Tipo.Equals(TipoVehiculo.Carro) && v.Valor == 0) >= CeldasCarro)
                || (tipo == TipoVehiculo.Moto && Vehiculos.Count(v => v.Tipo.Equals(TipoVehiculo.Moto) && v.Valor == 0) >= CeldasMoto))
            {
                return true;
            }
            return false;
        }
    }
}
