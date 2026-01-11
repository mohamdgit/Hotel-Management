using AutoMapper;
using AutoMapper.Features;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class HotelFeatureservice : IHotelFeatureService
    {
        private readonly IUow uow;
        private readonly IHotelFeatureReposatory hotelrepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HotelFeatureservice(IUow uow, IHotelFeatureReposatory hotelrepo,IMapper mapper,IHttpContextAccessor _httpContextAccessor)
        {
            this.uow = uow;
            this.hotelrepo = hotelrepo;
            this.mapper = mapper;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<int> Add(AddHotelFeaturesDto dto)
        {
            var Hotel = await uow.GenerateRepo<Hotel, int>().GetById(dto.HotelId);
            if(Hotel is not null)
            {
                if (GetuserId() == Hotel.managerId)
                {
                    await hotelrepo.AddHotelFeaturesAsync(dto.FeatureIds, dto.HotelId);

                }
                else
                {
                    throw new Exception("not allowed to you");
                }
            }
            else
            {
                throw new Exception("Hotel is not found");

            }
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
            var Hotel = await uow.GenerateRepo<Hotel, int>().GetById(hotelId);

            if (Hotel is not null)
            {
                if (GetuserId() == Hotel.managerId|| GetuserRole()=="SuperAdmin")
                {
                    await hotelrepo.Delete(featureId, hotelId);

                }
                else
                {
                    throw new Exception("not allowed to you");
                }
            }
            else
            {
                throw new Exception("Hotel is not found");

            }
            return await uow.SaveChanges();

        }
        private Guid GetuserId()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }
        private string GetuserRole()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userroleClaim = user.FindFirst(ClaimTypes.Role);

            if (userroleClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return userroleClaim.Value;
        }
        
    }
}
