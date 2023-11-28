using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class StudyProcessConfiguration : IEntityTypeConfiguration<StudyProcess>
    {
        public void Configure(EntityTypeBuilder<StudyProcess> builder)
        {
            builder.HasKey(sp => sp.Id);
            builder.HasOne(sp => sp.StudyCard).WithMany(sc => sc.StudyProcesses).HasForeignKey(sp => sp.StudyCardId).IsRequired();
            builder.HasOne(sp => sp.StudyDeck).WithMany(sd => sd.StudyProcesses).HasForeignKey(sp => sp.StudyDeckId).IsRequired();
            builder.HasOne(sp => sp.User).WithMany(u => u.StudyProcesses).HasForeignKey(sp => sp.UserId).IsRequired();
            builder.Property(sp => sp.Repetitions).IsRequired();
            builder.Property(sp => sp.Learning).IsRequired();
            builder.Property(sp => sp.EFactor).IsRequired();
            builder.Property(sp => sp.NextStudy).IsRequired();
        }
    }
}
