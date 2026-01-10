using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Specification
{
    public class HotelSpecification : BaseSpecification<Hotel, int> 
    {
        public HotelSpecification(itemsQueryParam? param)
     : base(p => param == null||(
        (string.IsNullOrEmpty(param.Name)
            || p.Name.ToLower().Contains(param.Name.ToLower()))


        && (string.IsNullOrEmpty(param.Location)
            || p.Location.ToLower().Contains(param.Location.ToLower()))

        && (!param.Rate.HasValue
            || p.Rate == param.Rate)&& (!param.HotelId.HasValue
            || p.Id == param.HotelId)
    ))
        {

            Addincludesfunc(p => p.Hotel_Features);
            Addincludesfunc(p => p.Reviews);
            Addincludesfunc(p => p.HotelRooms);
            Addincludesfunc(p => p.Bookings);
            Addincludesfunc(p => p.Manager);
            Addincludesfunc(p => p.Photos);
          


        }

    }
}
