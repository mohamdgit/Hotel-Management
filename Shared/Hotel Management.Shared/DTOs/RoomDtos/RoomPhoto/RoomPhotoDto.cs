using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto
{
    public class RoomPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int RoomId { get; set; }
    }

}
