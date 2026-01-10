using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.RoomService
{
    public interface IRoomService
    {
        public IEnumerable<RoomListDto> GtRoomsSpecification(itemsQueryParam?param);
        public IEnumerable<RoomListDto> GeTRooms();
        public Task<int> AddRoom(RoomCreateDto dto);
        public Task<int> updateRoom(RoomUpdateDto dto);
        public Task<int> DeleteRoom(int Id);
        public Task<RoomDetailsDto> GeTRoomById(int Id);

        
    }
}
