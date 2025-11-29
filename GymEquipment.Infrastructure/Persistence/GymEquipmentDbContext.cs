using GymEquipment.Domain.ForbiddenPhrases;
using GymEquipment.Domain.History;
using GymEquipment.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace GymEquipment.Infrastructure.Persistence
{
    public class GymEquipmentDbContext(DbContextOptions<GymEquipmentDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ForbiddenPhrase> ForbiddenPhrases => Set<ForbiddenPhrase>();
        public DbSet<ProductHistoryEntry> ProductHistoryEntries => Set<ProductHistoryEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymEquipmentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
