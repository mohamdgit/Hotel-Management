using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.Hotel.HotelController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HotelController(IServiceManager service):ControllerBase
    {
        [HttpGet("GetallHotels")]

        public  ActionResult<IEnumerable<HotelDto>> GetallHotels()
        {
           var res=  service.serviceOfHotel.GetallHotelsAsync();
            return Ok(res);
        }
        [HttpGet("Spechotel")]

        public ActionResult<IEnumerable<HotelDto>> GetallHotelswithspec([FromBody]itemsQueryParam ?param)
        {
            var res = service.serviceOfHotel.GetallHotelspecAsync(param);
            return Ok(res);
        }
        [HttpGet("Gethotel")]

        public async Task<ActionResult<HotelDto>> GetHotelByID(int Id)
        {
            var res = await service.serviceOfHotel.GetHotelByIdAsync(Id);
            return Ok(res);
        }
        [HttpPost("AddHotel")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult<int>> AddHotel([FromBody] AddHotelDto hotel)
        {
            var res = await service.serviceOfHotel.AddHotelAsync(hotel);
            return Ok(res);
        }
        [HttpPut("UpdateHotel/{id}")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult<int>> UpdateHotel( int id , [FromBody]  AddHotelDto dto)
         { 
             var res = await service.serviceOfHotel.UpdateHotel(id,dto);
              return Ok(res);
         }
        [HttpDelete("DeleteHotel")]
        [Authorize(Policy = "AdminOrSuperAdmin")]
        

        public async Task<ActionResult<int>> DeleteHotel(int id)
        {
            var res = await service.serviceOfHotel.deleteHotel(id);
            return Ok(res);
        }
    }
}
