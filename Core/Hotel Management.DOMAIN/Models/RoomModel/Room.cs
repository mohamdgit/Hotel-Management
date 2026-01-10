using Hotel_Management.DOMAIN.Models.BaseEntity;
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
    public class Room : BaseEntity<int>
    {
        public int Num { get; set; }
        public State RoomState { get; set; } = State.Avaliable;

        public int RoomTypeId { get; set; }

        [ForeignKey(nameof(RoomTypeId))]
        public  RoomType RoomType { get; set; }

          public decimal PricePerNight { get; set; }

        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        [InverseProperty("HotelRooms")]
        public  Hotel Hotel { get; set; }

        [InverseProperty("RoomBooked")]
        public  ICollection<Book>? Bookings { get; set; } = new HashSet<Book>();

        [InverseProperty("Room")]
        public  ICollection<RoomPhoto> Photos { get; set; } = new HashSet<RoomPhoto>();

        [InverseProperty("Room")]
        public  ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
    }

}

