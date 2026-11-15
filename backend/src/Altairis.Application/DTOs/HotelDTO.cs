using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; }
        public IEnumerable<RoomTypeDTO>? RoomTypes { get; set; } = null;
    }
}
