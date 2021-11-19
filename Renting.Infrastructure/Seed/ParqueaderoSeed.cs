using Renting.Domain.Entities;
using System.Collections.Generic;

namespace Renting.Infrastructure.Seed
{
    public static class ParqueaderoSeed
    {
        public static IEnumerable<Parqueadero> ObtenerParqueaderoSeeds()
        {
            return new List<Parqueadero>
            {
                new Parqueadero
                {
                    CeldasCarro = 20,
                    CeldasMoto = 10
                }
            };
        }
    }
}
