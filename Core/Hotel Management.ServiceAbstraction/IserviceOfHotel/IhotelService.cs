using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    public interface IHotelService
    {
        public  IEnumerable<HotelDto>GetallHotelsAsync();
        public IEnumerable<HotelDto> GetallHotelspecAsync(itemsQueryParam? param);


        public Task<HotelDto> GetHotelByIdAsync(int Id);

        public Task<int> AddHotelAsync(AddHotelDto hotel);
        public Task<int> UpdateHotel(int id, AddHotelDto dto);
        public Task<int> deleteHotel(int Id);


    }
}
