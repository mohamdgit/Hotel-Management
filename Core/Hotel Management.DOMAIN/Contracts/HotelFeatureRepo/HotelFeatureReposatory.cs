using Hotel_Management.DOMAIN.Models.HotelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo
{
    public interface IHotelFeatureReposatory
    {
        public Task<IEnumerable<HotelFeatures>> GetAllHotelFeaturesAsync(int hotelId);
        Task AddHotelFeaturesAsync(IEnumerable<int> ids, int hotelId);
        public Task Delete(int featureid, int hotelId);
    }
}
