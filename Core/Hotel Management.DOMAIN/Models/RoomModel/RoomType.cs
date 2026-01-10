using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.RoomModel
{
using System;
    public class RoomType:BaseEntity<int>
    {

        public string Name { get; set; }
        public string? Description { get; set; }

        public decimal BasePrice { get; set; }
        public int MaxGuests { get; set; }
        public int BedsCount { get; set; }
        public int Area { get; set; }

        public bool HasBalcony { get; set; }
        public bool HasSeaView { get; set; }
        public bool HasBathroomTub { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public  ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

    }
}
