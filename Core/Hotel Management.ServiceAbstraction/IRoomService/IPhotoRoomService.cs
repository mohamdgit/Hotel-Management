using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    public interface IPhotoRoomService
    {
        public Task<string> AddPhoto( string folder, RoomPhotoCreateDto dto);

        public  Task<bool> deletePhoto(String folder,string filepath, int photoid);
        public IEnumerable<RoomPhotoDto> getallphotos();

    }
}
