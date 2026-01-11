using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly IUow uow;
        private readonly IMapper mapp;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoRoomService photoservice;

        public HotelService(IUow uow,IMapper mapp,IHttpContextAccessor _httpContextAccessor)
        {
            this.uow = uow;
            this.mapp = mapp;
            this._httpContextAccessor = _httpContextAccessor;
            this.photoservice = photoservice;
        }
        public async Task<int> AddHotelAsync(AddHotelDto hotel)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
          var res=  mapp.Map<AddHotelDto, Hotel>(hotel);
            if (hotel.managerId== GetuserId())
            {
                await repo.Add(res);

            }
            else
            {
                throw new Exception("Not Allowed To You");
            }
                return await uow.SaveChanges();
        }
      
        public async Task<int> deleteHotel(int Id)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var hotel = await repo.GetById(Id);
            var user = _httpContextAccessor.HttpContext?.User;

            
            if (hotel.managerId == GetuserId()|| GetuserRole() == "SuperAdmin")
            {
                repo.Delete(Id); ;

            }
            else
            {
                throw new Exception("Not Allowed To You");
            }
            return await uow.SaveChanges();
        }

        
        public   IEnumerable<HotelDto> GetallHotelspecAsync(itemsQueryParam? param)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var spec = new HotelSpecification(param);
            var query =  repo.GetAllSpecificationAsync(spec).ToList();
            var res = mapp.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(query);
            
            return res;
        }

        public IEnumerable<HotelDto> GetallHotelsAsync()
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var query = repo.GetAllAsync().ToList();
            var res = mapp.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(query);

            return res;
        }

        public async Task<HotelDto> GetHotelByIdAsync(int Id)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var hotel= await repo.GetById(Id);
            var res = mapp.Map<Hotel, HotelDto>(hotel);

            return res;
        }

        public async Task<int> UpdateHotel(int id,AddHotelDto dto)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var hotel = await repo.GetById(id);
            if(hotel is not null)
            {
               mapp.Map(dto, hotel); ;
                if (hotel.managerId == GetuserId() )
                {
                    repo.Update(hotel); ;

                }
                else
                {
                    throw new Exception("Not Allowed To You");
                }
                return await uow.SaveChanges();

            }
            throw new Exception("Hotel not found");
        }
        private Guid GetuserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }
        private string GetuserRole()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userroleClaim = user.FindFirst(ClaimTypes.Role);

            if (userroleClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return userroleClaim.Value;
        }

    }
}
