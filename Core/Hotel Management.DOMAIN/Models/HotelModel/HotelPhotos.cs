using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class HotelPhotos
    {
        public int Id { get; set; }

      

        public string Url { get; set; } = null!; 
        public decimal Size { get; set; }
       
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        [InverseProperty("Photos")]
        public virtual Hotel Hotelsofphotos { get; set; } = null!;

    }
}
