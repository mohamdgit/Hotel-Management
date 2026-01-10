using AutoMapper;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.BooksDtos;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Hotel_Management.ServiceImplementiton.AutoMapping
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            /*Hotel Mapping*/
            // Hotel -> HotelDto )
            CreateMap<Hotel, HotelDto>()
                .ForMember(d => d.Features, o => o.MapFrom(P=>P.Hotel_Features.Select(P=>P.Feature)));
           
 ;

            // Hotel -> AddHotelDto
            CreateMap<Hotel, AddHotelDto>()
                .ForMember(p => p.managerId, o => o.MapFrom(p => p.managerId))
                .ReverseMap();

            /* ApplicationUser Mappings*/
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
                .ReverseMap();

            /* Feature Mappings */
            CreateMap<Feature, FeaturesDto>().ReverseMap();
            CreateMap<Feature, AddFeaturesDto>().ReverseMap();

            /* HotelFeatures Mappings*/
            // HotelFeatures
            CreateMap<HotelFeatures, HotelFeaturesDto>()
                .ForMember(d => d.HotelId, o => o.MapFrom(s => s.HotelId))
                .ForMember(d => d.HotelName, o => o.MapFrom(s => s.HotelOfFeatures.Name))
                .ForMember(d => d.FeatureIds, o => o.MapFrom(s => new int[] { s.FeatureId }))
                .ForMember(d => d.FeatureNames, o => o.MapFrom(s => new string[] { s.Feature.Name }));

            //  Hotel → HotelFeaturesDto
            CreateMap<Hotel, HotelFeaturesDto>()
                .ForMember(d => d.HotelId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.HotelName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.FeatureIds, o => o.MapFrom(s => s.Hotel_Features.Select(hf => hf.FeatureId)))
                .ForMember(d => d.FeatureNames, o => o.MapFrom(s => s.Hotel_Features.Select(hf => hf.Feature.Name)));

            /*HotelPhotos Mappings */
            CreateMap<HotelPhotos, HotelPhotoDto>().ForMember(d => d.Name, o => o.MapFrom<ImageUrlResolver > ()).
                ReverseMap();
            CreateMap<HotelPhotos, AddHotelPhotoDto>().ForMember(d => d.image, o => o.MapFrom(s => s.Name)).
                ReverseMap();
            CreateMap<UpdateHotelPhotoDto, HotelPhotos>().ForMember(d => d.Name, o => o.MapFrom(s => s.image)).
                ReverseMap();
            // Room -> RoomListDto
            CreateMap<Room, RoomListDto>()
                .ForMember(dest => dest.RoomState,
                    opt => opt.MapFrom(src => (int)src.RoomState))
                .ForMember(dest => dest.RoomTypeName,
                    opt => opt.MapFrom(src => src.RoomType.Name)).ForMember(dest => dest.Hotelname,
                    opt => opt.MapFrom(src => src.Hotel.Name)).ReverseMap();

           

            CreateMap<Room, RoomDetailsDto>()
                .ForMember(dest => dest.RoomState,
                    opt => opt.MapFrom(src => (int)src.RoomState)).ForMember(p=>p.PricePerNight,p=>p.Ignore());

            // RoomCreateDto -> Room
            CreateMap<RoomCreateDto, Room>()
                .ForMember(dest => dest.RoomState,
                    opt => opt.MapFrom(src => (State)src.RoomState));

            // RoomUpdateDto -> Room
            CreateMap<RoomUpdateDto, Room>()
                .ForMember(dest => dest.RoomState,
                    opt => opt.MapFrom(src => (State)src.RoomState));

            
            // RoomPhoto
            CreateMap<RoomPhoto, RoomPhotoDto>().ForMember(d => d.Url, o => o.MapFrom<ImageRoomUrlResolver>()).ReverseMap();
            CreateMap<RoomPhotoCreateDto, RoomPhoto>().ReverseMap();

            // RoomType
            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
            CreateMap<RoomType, GetRoomTypeDto>().ReverseMap();
            // Book
            CreateMap<Book, BookDto>()
     .ForMember(
         d => d.NumOfDays,
         o => o.MapFrom(s => (s.Todate - s.Fromdate).Days)
         ).ForMember(
        d => d.roomids,
        o => o.MapFrom(s => s.RoomBooked.Select(r => r.Id))
    );

            CreateMap<AddBookDto, Book>()
              .ForMember(d => d.RoomBooked, o => o.Ignore())
              .ForMember(d => d.Bookstate, o => o.Ignore())
              .ForMember(d => d.Createdat, o => o.Ignore());


        }
    }
}
