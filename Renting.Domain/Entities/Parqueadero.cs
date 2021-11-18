using System;
using System.Collections.Generic;

namespace Renting.Domain.Entities
{
    public class Parqueadero : EntityBase<Guid>
    {        
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
    }
}
