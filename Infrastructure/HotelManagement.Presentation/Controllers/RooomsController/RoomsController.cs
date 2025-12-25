using Hotel_Management.ServiceAbstraction.RoomService;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }


    [HttpGet("GetALl")]
    public IActionResult GetRooms()
    {
        var rooms = _roomService.GeTRooms();
        return Ok(rooms);
    }

    [HttpGet("specification")]
    public IActionResult GetRoomsSpecification()
    {
        var rooms = _roomService.GtRoomsSpecification();
        return Ok(rooms);
    }

    [HttpGet("GetRoom/{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var room = await _roomService.GeTRoomById(id);
        return Ok(room);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddRoom([FromBody] RoomCreateDto dto)
    {
        var result = await _roomService.AddRoom(dto);
        return Ok(new { Success = result > 0 });
    }

   

    [HttpPut("edit")]
    public async Task<IActionResult> UpdateRoom([FromBody] RoomUpdateDto dto)
    {
        var result = await _roomService.updateRoom(dto);
        return Ok(new { Success = result > 0 });
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var result = await _roomService.DeleteRoom(id);
        return Ok(new { Success = result > 0 });
    }
}