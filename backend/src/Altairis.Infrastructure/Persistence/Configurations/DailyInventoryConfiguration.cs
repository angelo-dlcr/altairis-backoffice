using Altairis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Infrastructure.Persistence.Configurations
{
    public class DailyInventoryConfiguration : IEntityTypeConfiguration<DailyInventory>
    {
        public void Configure(EntityTypeBuilder<DailyInventory> builder)
        {
            builder.ToTable("DailyInventory", table =>
            {
                table.HasCheckConstraint(
                    "CK_DailyInventory_TotalUnits",
                    "[TotalUnits] >= 0"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StayDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.TotalUnits)
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            //builder.HasOne(x => x.RoomType)
            //    .WithMany(x => x.DailyInventories)
            //    .HasForeignKey(x => x.RoomTypeId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasConstraintName("FK_DailyInventory_RoomTypes_RoomTypeId");

            builder.HasIndex(x => new { x.RoomTypeId, x.StayDate })
                .IsUnique()
                .HasDatabaseName("UX_DailyInventory_RoomTypeId_StayDate");

            builder.HasIndex(x => x.StayDate)
                .HasDatabaseName("IX_DailyInventory_StayDate");

            builder.Navigation(x => x.RoomType)
                .AutoInclude();
        }
    }
}
