using Hotel_Management.ServiceAbstraction.IRoomTypeService;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.ServiceImplementiton.Services.RoomTypeService;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.RoomControllers.RoomTypesController
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RoomTypesController(IServiceManager ServiceManager) : ControllerBase
    {

       

        [HttpGet("Get")]
        [Authorize(Policy = "Admin")]
        
        public ActionResult<IEnumerable< RoomTypeDto>> GetRoomTypes()
        {
            var types =  ServiceManager.RoomTypeService.GetRoomsType();
            return Ok(types);
        }

        [HttpGet("Get/{id}")]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<ActionResult<RoomTypeDto>> GetRoomTypeById(int id)
        {
            var type = await ServiceManager.RoomTypeService.GetRoomTypeById(id);
            return Ok(type);
        }
      
        [HttpPost("Add")]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<ActionResult<int>> AddRoomType([FromBody] RoomTypeDto dto)
        {
            var result = await ServiceManager.RoomTypeService.AddRoomType(dto);
            return Ok(result);
        }

       
    }
}
