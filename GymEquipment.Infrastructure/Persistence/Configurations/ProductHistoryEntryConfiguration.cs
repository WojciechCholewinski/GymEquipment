using GymEquipment.Domain.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymEquipment.Infrastructure.Persistence.Configurations
{
    public class ProductHistoryEntryConfiguration : IEntityTypeConfiguration<ProductHistoryEntry>
    {
        public void Configure(EntityTypeBuilder<ProductHistoryEntry> builder)
        {
            builder.ToTable("ProductHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.ChangeType)
                .HasConversion<int>() // enum -> int
                .IsRequired();

            builder.Property(x => x.ChangedAtUtc)
                .IsRequired();

            builder.Property(x => x.ChangedBy)
                .HasMaxLength(200);

            builder.Property(x => x.OldValue)
                .HasMaxLength(2000);

            builder.Property(x => x.NewValue)
                .HasMaxLength(2000);

            builder.HasIndex(x => x.ProductId);
        }
    }
}
