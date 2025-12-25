using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes
{
    public class GetRoomTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal BasePrice { get; set; }
        public int MaxGuests { get; set; }
        public int BedsCount { get; set; }
        public int Area { get; set; }

        public bool HasBalcony { get; set; }
        public bool HasSeaView { get; set; }
        public bool HasBathroomTub { get; set; }
    }
}
