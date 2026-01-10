using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.PaymentModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.BookModel
{
    public class Book : BaseEntity<int>
    {
        public BookState Bookstate { get; set; }
        public DateTime Createdat { get; set; }

        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public DateTime ExpiresAt { get; set; }

        /********************************************/
        // 1) User (One User → Many Books)
        /********************************************/
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Books")]
        public  ApplicationUser User { get; set; }

        /********************************************/
        // 2) Room (One Room → Many Bookings)
        /********************************************/
       
        [InverseProperty("Bookings")] 
        public ICollection<Room>? RoomBooked { get; set; } = new HashSet<Room>();

        /********************************************/
        // 3) Reviews (One Book → Many Reviews)
        /********************************************/
        [InverseProperty("BookReview")]
        public  ICollection<Review>? ReviewBook { get; set; } = new HashSet<Review>();

        /********************************************/
        // 4) Hotel (One Hotel → Many Bookings)
        /********************************************/
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        [InverseProperty("Bookings")]
        public  Hotel Hotel { get; set; }

        /********************************************/
        // 5) Payment (One Book → One Payment)
        /********************************************/
        [InverseProperty(nameof(Payement.PaymentBook))]
        public  Payement? BookPayment { get; set; }

        /********************************************/
        public bool IsCanceled { get; set; }
        public bool IsDeleted { get; set; }
    }

}
