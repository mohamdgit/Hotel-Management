using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.RoomModel
{
    public class Room
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public State RoomState { get; set; }
        public RoomType Roomtype { get; set; }
        public int RoomCapacity { get; set; }
        
        public int Hotelid { get; set; }
        [ForeignKey("Hotelid")]
        [InverseProperty("HotelRooms")]
        public virtual Hotel HotelofRoom { get; set; }

        [InverseProperty("RoomBooked")]
        public virtual Book? BookOfTheRoom { get; set; }

        [InverseProperty("Room")]
        public virtual ICollection<RoomPhoto>? Roomofphotos { get; set; } = new HashSet<RoomPhoto>();
        
    }
}
