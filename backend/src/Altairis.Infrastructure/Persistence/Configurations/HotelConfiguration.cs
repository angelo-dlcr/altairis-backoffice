using Altairis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Infrastructure.Persistence.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Address)
                .HasMaxLength(250);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(x => x.Code)
                .IsUnique()
                .HasDatabaseName("UX_Hotels_Code");

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Hotels_Name");

            builder.HasIndex(x => new { x.Country, x.City })
                .HasDatabaseName("IX_Hotels_Country_City");

            builder.Navigation(x => x.RoomTypes)
                .AutoInclude();
        }
    }
}
