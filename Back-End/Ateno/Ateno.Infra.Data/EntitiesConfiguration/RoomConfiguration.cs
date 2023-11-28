using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(32).IsRequired();
            builder.Property(r => r.Code).HasMaxLength(16);
            builder.HasOne(r => r.Admin).WithMany(u => u.Rooms).HasForeignKey(sc => sc.AdminId).IsRequired();
            builder.Property(r => r.IsPublic).IsRequired();
        }
    }
}