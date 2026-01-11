using AutoMapper;
using AutoMapper.Features;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class FeaturesService : IFeatureService
    {
        private readonly IUow uow;
        private readonly IMapper mapper;
       

        public FeaturesService(IUow uow,IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<int> AddFeatureAsync(AddFeaturesDto features)
        {
            var featurerepo = uow.GenerateRepo<Feature, int>();
            var feature = featurerepo.Add(mapper.Map<AddFeaturesDto, Feature>(features));
            return await uow.SaveChanges();
        }

        public  IEnumerable<FeaturesDto> GetFeatureAsync()
        {
            var featurerepo = uow.GenerateRepo<Feature, int>();
            var feature =  featurerepo.GetAllAsync();
            var result = mapper.Map<IEnumerable<Feature>, IEnumerable<FeaturesDto>>(feature).ToList();
           return result;
        }
        
    }
}
