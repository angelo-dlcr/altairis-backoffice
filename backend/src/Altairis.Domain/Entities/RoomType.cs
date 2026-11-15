using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Domain.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int MaxOccupancy { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; }
        public Hotel? Hotel { get; set; } = null;

        //public IEnumerable<DailyInventory>? DailyInventories { get; set; } = null;
        //public IEnumerable<Reservation>? Reservations { get; set; } = null;
    }
}
