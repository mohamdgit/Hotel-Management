using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Specification
{
    public class ReviewSpecification : BaseSpecification<Review, int>
    {
        public ReviewSpecification(itemsQueryParam? param)
  : base(p =>
  param != null&&
      ((!param.HotelId.HasValue) || p.Id == param.HotelId) &&
      ((!param.RoomId.HasValue) || p.RoomID == param.RoomId) &&
      ((!param.UserId.HasValue) || p.UserId == param.UserId)&&
      ((!param.ReviewId.HasValue) || p.Id == param.ReviewId) 


       )
        {
            Addincludesfunc(p => p.Hotel);
            Addincludesfunc(p => p.BookReview);
            Addincludesfunc(p => p.Users);
            Addincludesfunc(p => p.Room);
            OrderByAscFun(p => p.Stars);
            OrderByDescFun(p => p.Stars);


        }
    }
}
