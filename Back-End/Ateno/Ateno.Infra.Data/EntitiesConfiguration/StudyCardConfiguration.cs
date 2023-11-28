using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class StudyCardConfiguration : IEntityTypeConfiguration<StudyCard>
    {
        public void Configure(EntityTypeBuilder<StudyCard> builder)
        {
            builder.HasKey(sc => sc.Id);
            builder.HasOne(sc => sc.StudyDeck).WithMany(sd => sd.StudyCards).HasForeignKey(sc => sc.StudyDeckId).IsRequired();
            builder.Property(sc => sc.Front).HasMaxLength(2048).IsRequired();
            builder.Property(sc => sc.Back).HasMaxLength(2048).IsRequired();
        }
    }
}
