using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Domain.Entities
{
    public class DailyInventory
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public DateOnly StayDate { get; set; }
        public int TotalUnits { get; set; }
        public DateTime CreationDate { get; set; }

        public RoomType RoomType { get; set; } = null!;
    }
}
