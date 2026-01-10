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
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
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
            itemsQueryParam? item = new itemsQueryParam();
            item.RoomId =Id;

            var spec = new RoomSpecification(item);
            var room =  repo.GetAllSpecificationAsync(spec).FirstOrDefault();

          

            if (room.Hotel is not null&&room.Hotel.managerId == managerid )
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
            itemsQueryParam? item = new itemsQueryParam();
            item.RoomId = Id;

            var spec = new RoomSpecification(item);
            var room = repo.GetAllSpecificationAsync(spec).FirstOrDefault();
            if (room is not null)
            {
                var RoomTypeMapp = map.Map<Room, RoomDetailsDto>(room);
                RoomTypeMapp.PricePerNight = room.PricePerNight + RoomTypeMapp.RoomType.BasePrice;
                return RoomTypeMapp;
            }
            throw new Exception("Room not found");
        }

        public IEnumerable<RoomListDto> GeTRooms()
        {
            var repo = uow.GenerateRepo<Room, int>();
            var repotype = uow.GenerateRepo<RoomType, int>();
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
                foreach (var item in MappRooms)
                {
                    foreach (var r in rooms)
                    {
                        item.PricePerNight = item.PricePerNight + r.RoomType.BasePrice;

                    }

                }

                return MappRooms;
            }
            throw new Exception("Room not found");
        }

        public async Task<int> updateRoom(RoomUpdateDto dto)
        {
            var managerid = GetuserId();
            var repo = uow.GenerateRepo<Room, int>();
            itemsQueryParam? item = new itemsQueryParam();
            item.RoomId = dto.Id;
            var spec = new RoomSpecification(item);
            var room = repo.GetAllSpecificationAsync(spec).FirstOrDefault();
            if(room is null)
            {
                throw new Exception("room is not found");
            }

            

            if (room.Hotel.managerId == managerid)
            {
                map.Map(dto, room);
                repo.Update(room);
               return await uow.SaveChanges();
            }

            else
            {
                throw new Exception("not allowed to you");
            }
           
        }
    }
}
