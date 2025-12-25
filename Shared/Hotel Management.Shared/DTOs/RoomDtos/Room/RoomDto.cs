using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.Room
{

    public class RoomListDto
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public int RoomState { get; set; }
        public decimal PricePerNight { get; set; }

        public string RoomTypeName { get; set; }
        public int HotelId { get; set; }
    }
}


    
