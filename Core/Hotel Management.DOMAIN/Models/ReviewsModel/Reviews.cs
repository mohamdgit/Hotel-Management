using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.ReviewsModel
{
    public class Review
    {
        public int Id { get; set; }
       
       
        public int Stars { get; set; }
        public string? comment { get; set; }
        public DateTime createdat { get; set; } = DateTime.Now;
       
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("reviews")]
        public virtual ApplicationUser Users { get; set; }
        public int Bookid { get; set; }
        [ForeignKey("Bookid")]
        [InverseProperty("ReviewBook")]
        public virtual Book BookReview { get; set; }

       
        

    }
}
