using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.HotelModel
{
    public class Feature
    {
        public int Id { get; set; }
        public string Nmae { get; set; } = null!;
        [InverseProperty("Feature")]
        public virtual ICollection<HotelFeatures> HotelFeatures { get; set; } = new HashSet<HotelFeatures>();

    }
}
