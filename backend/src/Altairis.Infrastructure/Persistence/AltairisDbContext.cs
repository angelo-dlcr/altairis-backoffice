using Altairis.Application.Interfaces;
using Altairis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Infrastructure.Persistence
{
    public class AltairisDbContext : DbContext , IUnitOfWork
    {
        public AltairisDbContext(DbContextOptions<AltairisDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<DailyInventory> DailyInventory { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AltairisDbContext).Assembly);
        }
    }
}
