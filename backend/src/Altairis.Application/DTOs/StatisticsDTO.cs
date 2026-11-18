using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs
{
    public class StatisticsDTO
    {
        public int Hotels { get; set; } = 0;
        public int ActiveHotels { get; set; } = 0;
        public int InactiveHotels { get; set; } = 0;

        public int RoomTypes { get; set; } = 0;
        public int ActiveRoomTypes { get; set; } = 0;
        public int InactiveRoomTypes { get; set; } = 0;

        public int Reservations { get; set; } = 0;
        public int PendingReservations { get; set; } = 0;
        public int ConfirmedReservations { get; set; } = 0;
        public int CancelledReservations { get; set; } = 0;

        public int Customers { get; set; } = 0;
    }
}
