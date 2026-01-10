using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos
{
    public class UpdateHotelFeatureDto
    {
       
            public int HotelId { get; set; }
            public int OldFeatureId { get; set; }
            public int NewFeatureId { get; set; }
        
    }
}
