using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.ReviewsDtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int Stars { get; set; }
        public string? Title { get; set; }

        public string? comment { get; set; }
        public DateTime createdat { get; set; } = DateTime.Now;
        public int RoomId { get; set; }
        public int RoomNum { get; set; }

        // Hotel Info
        public int HotelId { get; set; }
        public string HotelName { get; set; }

        // Booking Info
        public int BookId { get; set; }
        public  string UserNmae { get; set; }
        // USER Info

        public Guid UserId { get; set; }


    }
}
