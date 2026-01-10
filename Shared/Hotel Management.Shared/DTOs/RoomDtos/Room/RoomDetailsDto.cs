using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.Room
{
    public class RoomDetailsDto
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public int RoomState { get; set; }
        public decimal PricePerNight { get; set; }

        public RoomTypeDto RoomType { get; set; }

        public List<RoomPhotoDto> Photos { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }

}
