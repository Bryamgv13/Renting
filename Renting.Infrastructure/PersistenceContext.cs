using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Renting.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Renting.Infrastructure
{
    public class PersistenceContext : DbContext
    {

        private readonly IConfiguration Config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));
            modelBuilder.Entity<Parqueadero>().HasKey(key => key.Id);
            modelBuilder.Entity<Vehiculo>();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var t = entityType.ClrType;
                if (typeof(DomainEntity).IsAssignableFrom(t))
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn").HasDefaultValueSql("GETDATE()");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn").HasDefaultValueSql("GETDATE()");
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
