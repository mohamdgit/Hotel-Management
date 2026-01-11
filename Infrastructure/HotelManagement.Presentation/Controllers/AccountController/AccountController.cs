using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_Management.Presentation.Controllers.AccountController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController(IServiceManager service) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] Logindto dto)
        {
            var Authservice=service.serviceOfAuthentication;
            var user = await Authservice.LoginAsync(dto);
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] Registerdto dto)
        {
            var Authservice = service.serviceOfAuthentication;
            var user = await Authservice.RegisterAsync(dto);
            return Ok(user);
        }
        [HttpGet("users")]
        [Authorize(policy:"SuperAdmin")]
        public  ActionResult<IEnumerable<UserDto>> GetUsersAsync()
        {
            var Authservice = service.serviceOfAuthentication;
            var users =  Authservice.GetUsers().ToList();

            return Ok(users);
        }
        [HttpPost("assignrole")]
        [Authorize(policy: "SuperAdmin")]

        public async Task<ActionResult<UserDto>> AssignRoleToUserAsync(string email,string role)
        {
            var Authservice = service.serviceOfAuthentication;
            var users = await Authservice.AssignRoleToUserAsync(email,role);

            return Ok(users);
        }

        [HttpPost("Forget")]
        public async Task<ActionResult> ForgetPasswords([FromQuery] ForgetPassword dto)
        {
            var Authservice = service.serviceOfAuthentication;
             await Authservice.ForgetPassword(dto);

            return Ok();
        }
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPassworddto dto)
        {
            var Authservice = service.serviceOfAuthentication;
           await Authservice.ResetPasswordAsync(dto);

            return Ok();
        }


    }
}
