using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.RoomDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelDtos
{
    public class AddHotelDto
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Location { get; set; } = null!;
       

        public Guid managerId { get; set; }
        
    }
}
