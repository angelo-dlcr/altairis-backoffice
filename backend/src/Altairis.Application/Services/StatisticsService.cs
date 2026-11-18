using Altairis.Application.DTOs;
using Altairis.Application.Helpers;
using Altairis.Application.Interfaces.Repositories;
using Altairis.Application.Interfaces.Services;
using Altairis.Domain.Entities;
using Altairis.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Altairis.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IBaseRepository<Hotel> _hotelService;
        private readonly IBaseRepository<RoomType> _roomTypeService;
        private readonly IBaseRepository<Reservation> _reservationService;

        public StatisticsService(
            IBaseRepository<Hotel> hotelService,
            IBaseRepository<RoomType> roomTypeService,
            IBaseRepository<Reservation> reservationService
        ) {
            _hotelService = hotelService;
            _roomTypeService = roomTypeService;
            _reservationService = reservationService;

        }

        public async Task<SingleApiResponse<StatisticsDTO>> GetStatisticsAsync()
        {
			try
			{
                var hotels = await _hotelService.GetAllAsync();
                var roomTypes = await _roomTypeService.GetAllAsync();
                var reservations = await _reservationService.GetAllAsync();

                StatisticsDTO statistics = new()
                {
                    Hotels = hotels?.Count ?? 0,
                    ActiveHotels = hotels?.Count(x => x.IsActive == true) ?? 0,
                    InactiveHotels = hotels?.Count(x => x.IsActive == false) ?? 0,

                    RoomTypes = roomTypes?.Count ?? 0,
                    ActiveRoomTypes = roomTypes?.Count(x => x.IsActive == true) ?? 0,
                    InactiveRoomTypes = roomTypes?.Count(x => x.IsActive == false) ?? 0,

                    Reservations = reservations?.Count ?? 0,
                    PendingReservations = reservations?.Count(x => x.Status == ReservationStatus.Pending) ?? 0,
                    ConfirmedReservations = reservations?.Count(x => x.Status == ReservationStatus.Confirmed) ?? 0,
                    CancelledReservations = reservations?.Count(x => x.Status == ReservationStatus.Cancelled) ?? 0,

                    Customers = reservations?.Where(x => !string.IsNullOrEmpty(x.GuestEmail)).GroupBy(x => x.GuestEmail).Count() ?? 0
                };

                return new SingleApiResponse<StatisticsDTO> { 
                    Data = statistics,
                };
			}
			catch (Exception ex)
            {

                return new SingleApiResponse<StatisticsDTO>
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                };
            }
        }
    }
}
