using Altairis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Altairis.Application.Interfaces;
using Altairis.Application.Interfaces.Repositories;
using Altairis.Infrastructure.Persistence.Repositories;
using Altairis.Domain.Entities;
using Altairis.Application.Interfaces.Services;
using Altairis.Application.Services;

namespace Altairis.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("The connection string was not found.");
            }

            services.AddDbContext<AltairisDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    sqlOptions => { sqlOptions.MigrationsAssembly(typeof(AltairisDbContext).Assembly.GetName().Name); }
                );
            });

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AltairisDbContext>());

            services.AddScoped<IBaseRepository<Hotel>, BaseRepository<Hotel>>();
            services.AddScoped<IBaseRepository<RoomType>, BaseRepository<RoomType>>();
            services.AddScoped<IBaseRepository<DailyInventory>, BaseRepository<DailyInventory>>();
            services.AddScoped<IBaseRepository<Reservation>, BaseRepository<Reservation>>();

            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IDailyInventoryService, DailyInventoryService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }
    }
}
