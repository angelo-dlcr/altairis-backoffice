using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs.Filters
{
    public class RoomTypeFiltersDTO
    {
        public int? HotelId { get; set; }
        public string? NameOrCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
    }
}
