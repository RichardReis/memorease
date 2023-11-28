using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(32);
            builder.Property(u => u.Name).HasMaxLength(128).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(128).IsRequired();
            builder.Property(u => u.DisabledAt);
        }
    }
}
