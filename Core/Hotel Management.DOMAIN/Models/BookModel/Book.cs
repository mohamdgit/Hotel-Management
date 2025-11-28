using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.PaymentModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.BookModel
{
    public class Book
    {
        public int Id { get; set; }
        public BookState Bookstate { get; set; }
        public int NumOfDays { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Books")]
        public virtual ApplicationUser User { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        [InverseProperty("BookOfTheRoom")]
        public virtual Room RoomBooked { get; set; }
     
        [InverseProperty("BookReview")]
        public virtual ICollection<Review>? ReviewBook { get; set; } = new HashSet<Review>();
        [InverseProperty(nameof(Payement.PaymentBook))]
        public virtual Payement? BookPayment { get; set; }


    }
}
