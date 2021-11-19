using Microsoft.Extensions.DependencyInjection;
using Renting.Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Renting.Domain.Ports;

namespace Renting.Infrastructure.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDataBase(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PersistenceContext>();
                if (!context.Set<Parqueadero>().Any())
                {
                    context.Set<Parqueadero>().AddRange(
                        ParqueaderoSeed.ObtenerParqueaderoSeeds()
                    );
                    context.SaveChanges();
                }
            }
        }

        public static void SeedTableStorage(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var repositorioTable = serviceScope.ServiceProvider.GetService<IRepositorioTable>();
                var picoYPlacas = PicoYPlacaSeed.ObtenerPicoYPlacaSeeds();
                foreach(var picoYPlaca in picoYPlacas)
                {
                    repositorioTable.UpsertPicoYPlaca(picoYPlaca);
                }
            }
        }
    }
}
