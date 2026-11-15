using Altairis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations", table =>
            {
                table.HasCheckConstraint(
                    "CK_Reservations_CheckOutAfterCheckIn",
                    "[CheckOutDate] > [CheckInDate]"
                );

                table.HasCheckConstraint(
                    "CK_Reservations_RoomsCount",
                    "[RoomsCount] > 0"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reference)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.GuestName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.GuestEmail)
                .HasMaxLength(254);

            builder.Property(x => x.CheckInDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.CheckOutDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.RoomsCount)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            //builder.HasOne(x => x.RoomType)
            //    .WithMany(x => x.Reservations)
            //    .HasForeignKey(x => x.RoomTypeId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasConstraintName("FK_Reservations_RoomTypes_RoomTypeId");

            builder.HasIndex(x => x.Reference)
                .IsUnique()
                .HasDatabaseName("UX_Reservations_Reference");

            builder.HasIndex(x => new
            {
                x.RoomTypeId,
                x.CheckInDate,
                x.CheckOutDate
            })
                .HasDatabaseName("IX_Reservations_RoomTypeId_Dates");

            builder.HasIndex(x => new { x.Status, x.CreationDate })
                .HasDatabaseName("IX_Reservations_Status_CreatedAtUtc");

            builder.Navigation(x => x.RoomType)
                .AutoInclude();
        }
    }
}
