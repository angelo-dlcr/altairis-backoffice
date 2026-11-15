using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelCode { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int MaxOccupancy { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; }
        //public HotelDTO? Hotel { get; set; } = null;
        //public IEnumerable<DailyInventoryDTO>? DailyInventories { get; set; } = null;
        //public IEnumerable<ReservationDTO>? Reservations { get; set; } = null;
    }
}
