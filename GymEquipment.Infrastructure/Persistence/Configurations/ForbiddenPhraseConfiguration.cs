using GymEquipment.Domain.ForbiddenPhrases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymEquipment.Infrastructure.Persistence.Configurations
{
    public class ForbiddenPhraseConfiguration : IEntityTypeConfiguration<ForbiddenPhrase>
    {
        public void Configure(EntityTypeBuilder<ForbiddenPhrase> builder)
        {
            builder.ToTable("ForbiddenPhrases");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Phrase)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Phrase)
                .IsUnique();
        }
    }
}
