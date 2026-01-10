using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.ProductQueryParam
{
    public class itemsQueryParam
    {
       public int? HotelId { get; set; }
        public int? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public int? ReviewId { get; set; }
        public int? BookId { get; set; }

        public String? Feature { get; set; }
        public BookStateDto? BookState { get; set; }
        public String? Name { get; set; }
        public decimal? Rate { get; set; }
        public String? Location { get; set; } 
        public int? PricePerNight { get; set; }
        public int?RoomState { get; set; }

    }
}
