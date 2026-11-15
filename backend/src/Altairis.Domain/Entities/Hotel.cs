using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; }

        public IEnumerable<RoomType>? RoomTypes { get; set; } = null;
    }
}
