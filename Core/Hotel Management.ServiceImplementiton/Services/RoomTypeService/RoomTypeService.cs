using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.ServiceAbstraction.IRoomTypeService;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.RoomTypeService
{
    public class RoomTypeService(IUow uow, IMapper mapper) : IRoomTypeService
    {
        public async Task<int> AddRoomType(RoomTypeDto dto)
        {
            var repo = uow.GenerateRepo<RoomType, int>();

            var RoomTypeMapp = mapper.Map<RoomTypeDto, RoomType>(dto);
            await repo.Add(RoomTypeMapp);
            return await uow.SaveChanges();
        }

        public async Task<int> DeleteRoomType(int Id)
        {
            var repo = uow.GenerateRepo<RoomType, int>();
             repo.Delete(Id);
            return await uow.SaveChanges();
        }

        public IEnumerable<RoomTypeDto> GetRoomsType()
        {
            var repo = uow.GenerateRepo<RoomType, int>();
            var roomtypes = repo.GetAllAsync();
            var RoomTypeMapp = mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeDto>>(roomtypes);
            return RoomTypeMapp;
        }

        public async Task<RoomTypeDto> GetRoomTypeById(int Id)
        {

            var repo = uow.GenerateRepo<RoomType, int>();
            
            var roomtypes = await repo.GetById(Id);
            if(roomtypes is not null)
            {
                var RoomTypeMapp = mapper.Map<RoomType,RoomTypeDto>(roomtypes);
                return RoomTypeMapp;
            }
            else
            {
                throw new Exception (" RoomType not exist");

            }

        }

    }
}
