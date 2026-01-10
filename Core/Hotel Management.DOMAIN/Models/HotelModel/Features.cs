using Hotel_Management.DOMAIN.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class Feature : BaseEntity<int>
    {
       
        public string Name { get; set; } = null!;
        [InverseProperty("Feature")]
        public  ICollection<HotelFeatures> HotelFeatures { get; set; } = new HashSet<HotelFeatures>();

    }
}
