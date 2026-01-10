using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos
{
    public class AddHotelFeaturesDto
    {
        public int HotelId { get; set; }
        public List<int> FeatureIds { get; set; } = new();
    }
}
