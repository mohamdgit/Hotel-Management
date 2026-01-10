using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;

namespace Hotel_Management.Shared.DTOs.Hotel.HotelDtos
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public Guid ManagerId { get; set; }
        public List<HotelPhotoDto> Photos { get; set; } = new List<HotelPhotoDto>();
        public List<FeaturesDto> Features { get; set; } = new List<FeaturesDto>();
        public List<RoomListDto> HotelRooms { get; set; } = new List<RoomListDto>();
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        

    }
}
