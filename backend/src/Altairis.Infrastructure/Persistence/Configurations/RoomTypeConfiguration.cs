using Altairis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Infrastructure.Persistence.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes", table =>
            {
                table.HasCheckConstraint(
                    "CK_RoomTypes_MaxOccupancy",
                    "[MaxOccupancy] > 0"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.MaxOccupancy)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            //builder.HasOne(x => x.Hotel)
            //    .WithMany(x => x.RoomTypes)
            //    .HasForeignKey(x => x.HotelId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasConstraintName("FK_RoomTypes_Hotels_HotelId");

            builder.HasIndex(x => new { x.HotelId, x.Code })
                .IsUnique()
                .HasDatabaseName("UX_RoomTypes_HotelId_Code");

            builder.HasIndex(x => x.HotelId)
                .HasDatabaseName("IX_RoomTypes_HotelId");

            //builder.Navigation(x => x.Hotel)
            //    .AutoInclude();

            //builder.Navigation(x => x.DailyInventories)
            //    .AutoInclude();

            //builder.Navigation(x => x.Reservations)
            //    .AutoInclude();
        }
    }
}
