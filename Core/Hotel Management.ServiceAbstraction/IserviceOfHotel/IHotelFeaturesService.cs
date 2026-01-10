using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    public interface IHotelFeatureService
    {
        Task<HotelFeaturesDto> GetHotelFeaturesAsync(int hotelId);

        public  Task<int> Add(AddHotelFeaturesDto dto);
        Task<int> RemoveFeatureFromHotelAsync(int hotelId, int featureId);
    }

}
