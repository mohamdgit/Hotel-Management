using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.PaymentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.PaymentController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize (policy:"User")]
        [HttpPost("{bookingId}")]

        public async Task<ActionResult<AddPaymentDto>> CreateOrUpdatePaymentIntent(int bookingId)
        {
            var payment = await serviceManager.PaymentServices.AddPayemantIntent(bookingId);
            return Ok(payment);

        }
    }
}
