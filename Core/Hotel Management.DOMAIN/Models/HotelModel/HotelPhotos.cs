using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class HotelPhotos : BaseEntity<int>
    {
        public string Name { get; set; } = null!; 
       
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        [InverseProperty("Photos")]
        public  Hotel Hotelsofphotos { get; set; } = null!;

    }
}
