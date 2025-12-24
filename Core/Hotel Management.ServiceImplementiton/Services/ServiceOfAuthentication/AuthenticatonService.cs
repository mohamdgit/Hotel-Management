using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.ServiceAbstraction.IserviceOfAuthentication;
using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using System.Security.Claims;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Hotel_Management.DOMAIN.Exceptions;

namespace Hotel_Management.ServiceImplementiton.Services.ServiceOfAuthentication
{
    public class AuthenticationService : IAuthenticatonService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration
           
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.configuration = configuration;
        }
        public async Task<UserDto> AssignRoleToUserAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (roleExists)
                {
                    var result = await _userManager.AddToRoleAsync(user, role);

                    if (result.Succeeded)
                    {
                        return new UserDto()
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            Token = Createtoken(user)
                        };
                    }
                    else
                    {
                        throw new UserNotFoundEx();
                    }

                }
                else
                {
                    throw new UserNotFoundEx();
                }

            }
            else
            {
                throw new UserNotFoundEx();
            }

          
          

           

           
        }
        public async Task ForgetPassword(ForgetPassword dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new UserNotFoundEx();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = Uri.EscapeDataString(token);

            var url = $"{configuration["Urls:baseurl"]}/api/Account/reset?email={dto.Email}&token={encoded}";

            var sen = new Emailsend
            {
                To = user.Email,
                Subject = "Reset Password",
                Body = $"Reset your password:\n{url}"
            };

            Helper.sendemail(sen);

        }
        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundEx();

            token = Uri.UnescapeDataString(token);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                throw new InvalidOperationException("Invalid token or password");
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users =  _userManager.Users.ToList().Select(dto=>new UserDto()
            {
                Id=dto.Id,
                UserName = dto.UserName,
                Fname = dto.Fname,
                Lname = dto.Lname,
                Phonenumber = dto.PhoneNumber,
                Email = dto.Email,
                Age = dto.Age,
            });
            return users;


        }
        public async Task<UserDto> LoginAsync(Logindto dto)
        {
            var user =await _userManager.FindByEmailAsync(dto.Email);
            if(user is not null){
                var flag = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (flag)
                {

                    return new UserDto()
                    {
                        UserName=user.UserName,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Phonenumber = user.PhoneNumber,
                        Email = user.Email,
                        Age = user.Age,
                        Token = Createtoken(user)
                    };
                }
                else
                {
                    throw new  UserNotFoundEx(); ;
                }
            }
            else
            {
                throw new UserNotFoundEx(); ;

            }
        }
        public async Task<UserDto> RegisterAsync(Registerdto dto)
        {
            var user = new ApplicationUser()
            {
                Fname = dto.Fname,
                Lname = dto.Lname,
                UserName=dto.UserName,
                
                PhoneNumber = dto.Phonenumber,
                Email = dto.Email,
                Age = dto.Age
            };
            var create = await _userManager.CreateAsync(user, dto.Password);
            if (create.Succeeded)
            {
                var returnuser = new UserDto()
                {
                    UserName = user.UserName,

                    Fname = user.Fname,
                    Lname = user.Lname,
                    Age = user.Age,
                    Phonenumber = user.PhoneNumber,
                    Email = user.Email,
                    IsBlocked = false,
                    Token = Createtoken( user)

                };
                await _userManager.AddToRoleAsync(user, "User");
                return returnuser;
            }
            else
            {
                create.Errors.Select(p => p.Description).ToList();
                throw new Exception() ;
            }
        }
        private string Createtoken(ApplicationUser user)
        {
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Fname),
                new Claim(ClaimTypes.Email,user.Email),

            };
            var roles = _roleManager.Roles.Select(p => p.Name).ToList();
            foreach (var item in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role,item));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwtauth")["securitykey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                   issuer: configuration.GetSection("jwtauth")["Issuer"],
                   audience: configuration.GetSection("jwtauth")["Audience"],
                   claims: claim,
                   expires: DateTime.UtcNow.AddMinutes(55),
                   signingCredentials: creds
               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
    
    }
}
