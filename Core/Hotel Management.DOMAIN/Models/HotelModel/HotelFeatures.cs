using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class HotelFeatures  
    {
    
        public int FeatureId { get; set; }
       
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        [InverseProperty("Hotel_Features")]
        public  Hotel HotelOfFeatures { get; set; } = null!;
        [ForeignKey("FeatureId")]
        [InverseProperty("HotelFeatures")]
        public  Feature Feature { get; set; } = null!;


    }
}
