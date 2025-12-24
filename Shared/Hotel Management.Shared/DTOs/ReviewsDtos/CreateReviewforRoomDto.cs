using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.ReviewsDtos
{
    public class CreateReviewforRoomDto
    {

        public int Stars { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public DateTime createdat { get; set; } = DateTime.Now;

        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int BookId { get; set; }
        public Guid UserId { get; set; }

    }
}
