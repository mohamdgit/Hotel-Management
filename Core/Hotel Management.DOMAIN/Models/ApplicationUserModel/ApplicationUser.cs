using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.ApplicationUserModel
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public int Age { get; set; }
        [InverseProperty("Manager")]
        public  Hotel? ManagedHotel { get; set; }
        [InverseProperty("Users")]
        public  ICollection<Review>? reviews { get; set; } = new HashSet<Review>();
        [InverseProperty("User")]
        public  ICollection<Book>? Books { get; set; } = new HashSet<Book>();
        public bool IsBlocked { get; set; }

    }
}
