using Hotel_Management.Shared.DTOs.Hotel.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    
        public interface IFeatureService
        {
            Task<int> AddFeatureAsync(AddFeaturesDto features);
           IEnumerable<FeaturesDto> GetFeatureAsync();

    }

}
