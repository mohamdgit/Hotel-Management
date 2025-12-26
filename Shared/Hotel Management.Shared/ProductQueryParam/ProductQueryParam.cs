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
        public String? Feature { get; set; }

        public String? Name { get; set; }
        public decimal? Rate { get; set; }
        public String? Location { get; set; } 
        public String? feature { get; set; }
        public int? PricePerNight { get; set; }
        public int?RoomState { get; set; }

    }
}
