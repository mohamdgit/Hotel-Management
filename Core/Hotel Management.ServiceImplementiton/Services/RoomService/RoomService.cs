using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.ServiceAbstraction.RoomService;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.RoomService
{
    public class RoomService(IUow uow,IMapper map, IHttpContextAccessor _httpContextAccessor) : IRoomService
    {

        private Guid GetuserId()
        {
            var userId = Guid.Parse(
    _httpContextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier).Value
);
            return userId;
        }
        public async Task<int> AddRoom(RoomCreateDto dto)
        {
            var managerid=GetuserId();
            var repo = uow.GenerateRepo<Room, int>();
            var Hotelrepo = uow.GenerateRepo<Hotel, int>();
            var hotel=await Hotelrepo.GetById(dto.HotelId);
            var AddRoom = map.Map<RoomCreateDto, Room>(dto);
            if(hotel.managerId == managerid && hotel is not null)
            {
                await repo.Add(AddRoom);

            }
            else
            {
                throw new Exception("not allowed to you");
            }
            return await uow.SaveChanges();
        }
        public async Task<int> DeleteRoom(int Id)
        {
            var managerid = GetuserId();
            var repo = uow.GenerateRepo<Room, int>();
            var Hotelrepo = uow.GenerateRepo<Hotel, int>();
            var room = await repo.GetById(Id);

            var hotel = await Hotelrepo.GetById(room.HotelId);

            if (hotel.managerId == managerid && hotel is not null)
            {
                repo.Delete(room.Id);

            }
            else
            {
                throw new Exception("not allowed to you");
            }
            return await uow.SaveChanges();

        }

        public async Task<RoomDetailsDto> GeTRoomById(int Id)
        {
            var repo = uow.GenerateRepo<Room, int>();
            var room = await repo.GetById(Id);
            if(room is not null)
            {
                var RoomTypeMapp = map.Map<Room, RoomDetailsDto>(room);
                return RoomTypeMapp;
            }
            throw new Exception("Room not found");
        }

        public IEnumerable<RoomListDto> GeTRooms()
        {
            var repo = uow.GenerateRepo<Room, int>();
            var rooms =  repo.GetAllAsync().ToList();
            if (rooms is not null)
            { 
                var MappRooms = map.Map< IEnumerable<Room>,IEnumerable<RoomListDto>>(rooms);
                return MappRooms;
            }
            throw new Exception("Room not found");
        }

        public IEnumerable<RoomListDto> GtRoomsSpecification(itemsQueryParam?param)
        {
            var repo = uow.GenerateRepo<Room, int>();
            var spec = new RoomSpecification(param);
            var rooms = repo.GetAllSpecificationAsync(spec).ToList();
            if (rooms is not null)
            {
                var MappRooms = map.Map<IEnumerable<Room>, IEnumerable<RoomListDto>>(rooms);
                return MappRooms;
            }
            throw new Exception("Room not found");
        }

        public async Task<int> updateRoom(RoomUpdateDto dto)
        {
            var managerid = GetuserId();
            var repo = uow.GenerateRepo<Room, int>();
            var Hotelrepo = uow.GenerateRepo<Hotel, int>();
            var room = await repo.GetById(dto.Id);

            var hotel = await Hotelrepo.GetById(room.HotelId);

            if (hotel.managerId == managerid && hotel is not null)
            {
                repo.Update(room);

            }
            else
            {
                throw new Exception("not allowed to you");
            }
          
            return await uow.SaveChanges();
        }
    }
}
