using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos
{
    public class UpdateHotelPhotoDto
    {
        public int Id { get; set; }
        public IFormFile image { get; set; } = null!;
        public int HotelId { get; set; }

    }
}
