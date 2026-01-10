using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Hotelname { get; set; }

        public List<BookDto>? Bookings { get; set; } = new List<BookDto>();

        public List<RoomPhotoDto> Photos { get; set; } = new List<RoomPhotoDto>();

        public List<ReviewDto>? Reviews { get; set; } = new List<ReviewDto>();
    }
}


    
