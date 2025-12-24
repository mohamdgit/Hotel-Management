using AutoMapper;
using AutoMapper.Features;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class HotelFeatureservice : IHotelFeatureService
    {
        private readonly IUow uow;
        private readonly IHotelFeatureReposatory hotelrepo;
        private readonly IMapper mapper;
        

        public HotelFeatureservice(IUow uow, IHotelFeatureReposatory hotelrepo,IMapper mapper)
        {
            this.uow = uow;
            this.hotelrepo = hotelrepo;
            this.mapper = mapper;
           
        }

        public async Task<int> Add(AddHotelFeaturesDto dto)
        {
            await hotelrepo.AddHotelFeaturesAsync(dto.FeatureIds,dto.HotelId);
            return await uow.SaveChanges();
        }

        public async Task<HotelFeaturesDto> GetHotelFeaturesAsync(int hotelId)
        {
            var features = await hotelrepo.GetAllHotelFeaturesAsync(hotelId);

            if (!features.Any())
                return null;

            var dto = new HotelFeaturesDto
            {
                HotelId = hotelId,
                HotelName = features.First().HotelOfFeatures.Name,
                FeatureIds = features.Select(f => f.FeatureId).ToList(),
                FeatureNames = features.Select(f => f.Feature.Name).ToList()
            };

            return dto;
        }

        public async Task<int> RemoveFeatureFromHotelAsync(int hotelId, int featureId)
        {
          await hotelrepo.Delete(featureId, hotelId);
            return await uow.SaveChanges();

        }
    }
}
