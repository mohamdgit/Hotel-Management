using Hotel_Management.ServiceAbstraction.RoomService;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]

public class RoomsController : ControllerBase
{
    private readonly IServiceManager service;

    public RoomsController(IServiceManager service)
    {
        this.service = service;
    }


    [HttpGet("GetALl")]
    public ActionResult<IEnumerable< RoomListDto>> GetRooms()
    {
        var rooms = service.RoomService.GeTRooms();
        return Ok(rooms);
    }

    [HttpGet("specification")]
    public ActionResult<IEnumerable< RoomListDto>> GetRoomsSpecification([FromQuery]itemsQueryParam?param)
    {
        var rooms = service.RoomService.GtRoomsSpecification(param);
        return Ok(rooms);
    }

    [HttpGet("GetRoom/{id}")]
    public async Task<ActionResult<RoomDetailsDto>> GetRoomById(int id)
    {
        var room = await service.RoomService.GeTRoomById(id);
        return Ok(room);
    }

    [HttpPost("Add")]

    [Authorize(Policy = "AdminOnly")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<int>> AddRoom([FromBody] RoomCreateDto dto)
    {
        var result = await service.RoomService.AddRoom(dto);
        return Ok(result);
    }




[HttpPut("edit")]
    [Authorize(Policy = "AdminOnly")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<int>> UpdateRoom([FromBody] RoomUpdateDto dto)
    {
        var result = await service.RoomService.updateRoom(dto);
        return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Policy = "AdminOnly")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<int>> DeleteRoom(int id)
    {
        var result = await service.RoomService.DeleteRoom(id);
        return Ok(result);
    }
}