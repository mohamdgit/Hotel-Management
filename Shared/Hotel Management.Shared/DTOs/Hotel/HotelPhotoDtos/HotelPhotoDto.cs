using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos
{
    public class HotelPhotoDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;


    }
}
