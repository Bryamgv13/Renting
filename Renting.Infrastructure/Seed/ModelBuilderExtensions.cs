using Microsoft.Extensions.DependencyInjection;
using Renting.Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace Renting.Infrastructure.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDataBase(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PersistenceContext>();
                if (!context.Set<PicoYPlaca>().Any())
                {
                    context.Set<PicoYPlaca>().AddRange(
                        PicoYPlacaSeed.ObtenerPicoYPlacaSeeds()
                    );
                    context.SaveChanges();
                }
                if (!context.Set<Parqueadero>().Any())
                {
                    context.Set<Parqueadero>().AddRange(
                        ParqueaderoSeed.ObtenerParqueaderoSeeds()
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
