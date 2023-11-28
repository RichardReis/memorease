using Ateno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.EntitiesConfiguration
{
    public class RoomUserConfiguration : IEntityTypeConfiguration<RoomUser>
    {
        public void Configure(EntityTypeBuilder<RoomUser> builder)
        {
            builder.HasKey(ru => ru.Id);
            builder.HasOne(ru => ru.Room).WithMany(r => r.RoomUsers).HasForeignKey(ru => ru.RoomId).IsRequired();
            builder.HasOne(ru => ru.User).WithMany(u => u.RoomUsers).HasForeignKey(ru => ru.UserId).IsRequired();
            builder.Property(ru => ru.CreatedAt).IsRequired();
        }
    }
}
