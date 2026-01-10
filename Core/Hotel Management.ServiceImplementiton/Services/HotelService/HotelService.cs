using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly IUow uow;
        private readonly IMapper mapp;
        private readonly IPhotoRoomService photoservice;

        public HotelService(IUow uow,IMapper mapp)
        {
            this.uow = uow;
            this.mapp = mapp;
            this.photoservice = photoservice;
        }
        public async Task<int> AddHotelAsync(AddHotelDto hotel)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
          var res=  mapp.Map<AddHotelDto, Hotel>(hotel);
             await repo.Add(res);
            return await uow.SaveChanges();
        }

        public async Task<int> deleteHotel(int Id)
        {
            var repo = uow.GenerateRepo<Hotel, int>();
            var hotel = await repo.GetById(Id);
         
            
             repo.Delete(Id); ;
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

                repo.Update(hotel); ;
                return await uow.SaveChanges();

            }
            throw new Exception("Hotel not found");
        }
      

    }
}
