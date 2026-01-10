using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Specification
{
    public class RoomSpecification : BaseSpecification<Room, int>
    {
        public RoomSpecification(itemsQueryParam? param)
    : base(p =>
   (param == null || !param.RoomState.HasValue || p.RoomState == (State)param.RoomState.Value)
        &&
        (param != null) &&
         (!param.PricePerNight.HasValue || p.PricePerNight == param.PricePerNight)
    &&
         (!param.RoomId.HasValue || p.Id == param.RoomId)
    )
           
     
        {
            Addincludesfunc(p => p.Hotel);
            Addincludesfunc(p => p.Reviews);
            Addincludesfunc(p => p.Bookings);
            Addincludesfunc(p => p.Bookings);
            Addincludesfunc(p => p.Photos);
            Addincludesfunc(p => p.RoomType);
            OrderByAscFun(p => p.PricePerNight);
            OrderByDescFun(p => p.PricePerNight);
        }
    }
}
