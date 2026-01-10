using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto
{
    public class RoomPhotoCreateDto
    {
        public IFormFile image { get; set; } = null!;
        public int RoomId { get; set; }
    }

}
