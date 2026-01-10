using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class Hotel : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Location { get; set; } = null!;

        // -----------------------------
        // Manager
        // -----------------------------
        public Guid managerId { get; set; }

        [ForeignKey("managerId")]
        [InverseProperty("ManagedHotel")]
        public  ApplicationUser Manager { get; set; } = null!;

        // -----------------------------
        // Photos
        // -----------------------------
        [InverseProperty("Hotelsofphotos")]
        public  ICollection<HotelPhotos> Photos { get; set; } = new HashSet<HotelPhotos>();

        // -----------------------------
        // Features
        // -----------------------------
        [InverseProperty("HotelOfFeatures")]
        public  ICollection<HotelFeatures> Hotel_Features { get; set; } = new HashSet<HotelFeatures>();

        // -----------------------------
        // Rooms
        // -----------------------------
        [InverseProperty("Hotel")]
        public  ICollection<Room> HotelRooms { get; set; } = new HashSet<Room>();

        // -----------------------------
        // Reviews
        // -----------------------------
        [InverseProperty("Hotel")]
        public  ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();

        // -----------------------------
        // Bookings
        // -----------------------------
        [InverseProperty("Hotel")]
        public  ICollection<Book>? Bookings { get; set; } = new HashSet<Book>();
    }

}
