using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos
{
    public class AddHotelPhotoDto
    {
        public int HotelId { get; set; }
        public IFormFile image { get; set; } = null!;
    }
}
