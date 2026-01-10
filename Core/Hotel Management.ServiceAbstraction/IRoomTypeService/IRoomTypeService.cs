using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IRoomTypeService
{
    public interface IRoomTypeService
    {
        public Task<int> AddRoomType(RoomTypeDto dto);
        public Task<int> DeleteRoomType(int Id);
        public Task<RoomTypeDto> GetRoomTypeById(int Id);

        public IEnumerable<RoomTypeDto> GetRoomsType();



    }
}
