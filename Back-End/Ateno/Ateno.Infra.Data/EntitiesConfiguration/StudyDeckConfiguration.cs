using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class StudyDeckConfiguration : IEntityTypeConfiguration<StudyDeck>
    {
        public void Configure(EntityTypeBuilder<StudyDeck> builder)
        {
            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Name).HasMaxLength(32).IsRequired();
            builder.HasOne(sd => sd.User).WithMany(u => u.StudyDecks).HasForeignKey(sc => sc.UserId);
            builder.HasOne(sd => sd.Room).WithMany(r => r.StudyDecks).HasForeignKey(sc => sc.RoomId);
            builder.Property(sd => sd.CreatedAt).IsRequired();
        }
    }
}