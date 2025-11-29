using GymEquipment.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymEquipment.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Type)
                .HasConversion<int>() // enum -> int
                .IsRequired();

            builder.Property(p => p.Category)
                .HasConversion<int>() // enum -> int
                .IsRequired();

            builder.Property(p => p.WeightKg)
                .HasColumnType("decimal(10,2)") // nullowalne
                .IsRequired(false);

            builder.Property(p => p.QuantityAvailable)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
