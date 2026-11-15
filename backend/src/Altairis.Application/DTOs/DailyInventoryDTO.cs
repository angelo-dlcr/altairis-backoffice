using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs
{
    public class DailyInventoryDTO
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int? HotelId { get; set; }
        public string? HotelCode { get; set; }
        public string? HotelName { get; set; }
        public string? RoomTypeCode { get; set; }
        public string? RoomTypeName { get; set; }
        public DateOnly StayDate { get; set; }
        public int TotalUnits { get; set; }
        public int ReservedUnits { get; set; }
        public int AvailableUnits { get; set; }
        public DateTime CreationDate { get; set; }

        //public RoomTypeDTO RoomType { get; set; } = null!;
    }
}
