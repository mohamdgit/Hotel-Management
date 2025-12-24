using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos
{
    public class HotelFeaturesDto
    {
        
            public int HotelId { get; set; }
            public string HotelName { get; set; }
            public List<int> FeatureIds { get; set; } = new();
            public List<string> FeatureNames { get; set; } = new();
        

    }
}
