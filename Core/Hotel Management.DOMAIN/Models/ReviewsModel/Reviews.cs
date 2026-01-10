using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.ReviewsModel
{
    public class Review : BaseEntity<int>
    {
       
       
        public int Stars { get; set; }
        public string? Title { get; set; }

        public string? comment { get; set; }
        public DateTime createdat { get; set; } = DateTime.Now;
       
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("reviews")]
        public  ApplicationUser Users { get; set; }
        public int Bookid { get; set; }
        [ForeignKey("Bookid")]
        [InverseProperty("ReviewBook")]
        public  Book BookReview { get; set; }
        public int HotelId { get; set; }
        
        [ForeignKey("HotelId")]
        [InverseProperty("Reviews")]
        public  Hotel Hotel { get; set; }

        public int? RoomID { get; set; }
        [ForeignKey("RoomID")]
        [InverseProperty("Reviews")]
        public  Room? Room { get; set; }





    }
}
