using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.Room
{

    public class RoomCreateDto
    {
        public int Num { get; set; }
        public int RoomState { get; set; }

        public decimal PricePerNight { get; set; }

        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
    }



}
