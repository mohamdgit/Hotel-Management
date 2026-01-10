using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfAuthentication
{
    public interface IAuthenticatonService
    {
        public Task<UserDto> LoginAsync(Logindto dto);
        public Task<UserDto> RegisterAsync(Registerdto dto);
        public IEnumerable<UserDto> GetUsers();
        public Task ForgetPassword(ForgetPassword dto);
        public  Task ResetPasswordAsync(ResetPassworddto dto);
        Task<UserDto> AssignRoleToUserAsync(string email, string role);



    }
}
