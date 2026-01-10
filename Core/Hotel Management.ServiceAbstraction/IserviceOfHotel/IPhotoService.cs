using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    public interface IPhotoService
    {
        public Task<string> AddPhoto( string folder, AddHotelPhotoDto dto);

        public  Task<bool> deletePhoto(String folder,string filepath, int photoid);
        public IEnumerable<HotelPhotoDto> getallphotos();

    }
}
