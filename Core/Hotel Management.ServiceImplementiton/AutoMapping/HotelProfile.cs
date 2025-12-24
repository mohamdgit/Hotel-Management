using AutoMapper;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using System.Linq;

namespace Hotel_Management.ServiceImplementiton.AutoMapping
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            /*************** Hotel Mappings ***************/
            // Hotel -> HotelDto (Features يتم map يدوي بعد الـ AutoMapper)
            CreateMap<Hotel, HotelDto>()
                .ForMember(d => d.Features, o => o.MapFrom(P=>P.Hotel_Features.Select(P=>P.Feature)));
           
 ;

            // Hotel -> AddHotelDto
            CreateMap<Hotel, AddHotelDto>()
                .ForMember(p => p.managerId, o => o.MapFrom(p => p.managerId))
                .ReverseMap();

            /*************** ApplicationUser Mappings ***************/
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
                .ReverseMap();

            /*************** Feature Mappings ***************/
            CreateMap<Feature, FeaturesDto>().ReverseMap();
            CreateMap<Feature, AddFeaturesDto>().ReverseMap();

            /*************** HotelFeatures Mappings ***************/
            // Mapping لكل HotelFeatures لعكس DTO
            CreateMap<HotelFeatures, HotelFeaturesDto>()
                .ForMember(d => d.HotelId, o => o.MapFrom(s => s.HotelId))
                .ForMember(d => d.HotelName, o => o.MapFrom(s => s.HotelOfFeatures.Name))
                .ForMember(d => d.FeatureIds, o => o.MapFrom(s => new int[] { s.FeatureId }))
                .ForMember(d => d.FeatureNames, o => o.MapFrom(s => new string[] { s.Feature.Name }));

            // Mapping من Hotel → HotelFeaturesDto
            CreateMap<Hotel, HotelFeaturesDto>()
                .ForMember(d => d.HotelId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.HotelName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.FeatureIds, o => o.MapFrom(s => s.Hotel_Features.Select(hf => hf.FeatureId)))
                .ForMember(d => d.FeatureNames, o => o.MapFrom(s => s.Hotel_Features.Select(hf => hf.Feature.Name)));

            /*************** HotelPhotos Mappings ***************/
            CreateMap<HotelPhotos, HotelPhotoDto>().ReverseMap();
            CreateMap<HotelPhotos, AddHotelPhotoDto>().ReverseMap();
            CreateMap<UpdateHotelPhotoDto, HotelPhotos>().ReverseMap();
        }
    }
}
