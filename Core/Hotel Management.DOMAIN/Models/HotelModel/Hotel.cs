using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
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
    public class Hotel
    {
        public int Id { get; set; }
        public String Name{ get; set; }
        public decimal Rate { get; set; }
        public String Description { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String Location { get; set; } = null!;
       
        public Guid managerId { get; set; }
        [ForeignKey("managerId")]
        [InverseProperty("ManagedHotel")]
        public virtual ApplicationUser Manager { get; set; }
        [InverseProperty("Hotelsofphotos")]
        public virtual ICollection<HotelPhotos>? Photos { get; set; } = new HashSet<HotelPhotos>();
        [InverseProperty("HotelOfFeatures")]
        public virtual ICollection<HotelFeatures> Hotel_Features { get; set; } = new HashSet<HotelFeatures>();
        [InverseProperty("HotelofRoom")]
        public virtual ICollection<Room>? HotelRooms { get; set; } = new HashSet<Room>();






    }
}
