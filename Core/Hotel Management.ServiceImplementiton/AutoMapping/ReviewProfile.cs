using AutoMapper;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.AutoMapping
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
           .ForMember(dest => dest.RoomNum, opt => opt.MapFrom(src => src.Room.Num))
           .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
           .ForMember(dest => dest.UserNmae, opt => opt.MapFrom(src => src.Users.UserName));
            /************************************************************************************/
            CreateMap<CreateReviewforHotelDto, Review>()
           .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
           .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(dest => dest.Bookid, opt => opt.MapFrom(src => src.BookId));
            /************************************************************************************/
            CreateMap<CreateReviewforRoomDto, Review>()
           .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
           .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(dest => dest.Bookid, opt => opt.MapFrom(src => src.BookId));
            /************************************************************************************/
            CreateMap<UpdateRoomReviewDto, Review>()
            .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Bookid, opt => opt.MapFrom(src => src.BookId));
        
            /************************************************************************************/

                    CreateMap<UpdateHotelReviewDto, Review>()
             .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
             .ForMember(dest => dest.Bookid, opt => opt.MapFrom(src => src.BookId));
  
  

        }

    }
}
