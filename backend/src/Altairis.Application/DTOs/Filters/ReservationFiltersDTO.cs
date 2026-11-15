using Altairis.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs.Filters
{
    public class ReservationFiltersDTO
    {
        public string? Reference { get; set; }
        public string? RoomTypeCode { get; set; }
        public string? GuestName { get; set; }
        public ReservationStatus? Status { get; set; }
        public DateOnly? CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
    }
}
