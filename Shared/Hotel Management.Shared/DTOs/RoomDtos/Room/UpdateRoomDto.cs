using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.Room
{
    public class RoomUpdateDto
    {
        public int Id { get; set; }

        public int Num { get; set; }
        public int RoomState { get; set; } = 2;
        public decimal PricePerNight { get; set; }
        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }
    }

}
