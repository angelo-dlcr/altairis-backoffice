using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs.Filters
{
    public class HotelFiltersDTO
    {
        public string? NameOrCode { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
