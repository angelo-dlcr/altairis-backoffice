using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs.Filters
{
    public class DailyInventoryFiltersDTO
    {
        public string? RoomTypeCode { get; set; }
        public string? HotelCodeOrName { get; set; }
        public DateOnly? StayDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
