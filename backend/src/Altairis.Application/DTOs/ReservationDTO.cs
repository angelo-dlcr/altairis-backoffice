using Altairis.Domain.Entities;
using Altairis.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string? GuestEmail { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int RoomsCount { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public DateTime CreationDate { get; set; }

        public RoomTypeDTO? RoomType { get; set; } = null!;
        public HotelDTO? Hotel { get; set; } = null!;
    }
}
