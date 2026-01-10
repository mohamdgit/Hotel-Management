using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.Seeding;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Seeding
{
    public class Seeding : ISeeding
    {
        private readonly HotelContext context;
        private readonly UserManager<ApplicationUser> user;
        private readonly RoleManager<IdentityRole<Guid>> role;

        public Seeding(HotelContext context,UserManager<ApplicationUser>user,RoleManager<IdentityRole<Guid>> role )
        {
            this.context = context;
            this.user = user;
            this.role = role;
        }
        public async Task SeedUserRoles()
        {
            var mig = await context.Database.GetAppliedMigrationsAsync();
            if (mig.Any())
            {
                context.Database.Migrate();
            }
            if (!role.Roles.Any())
            {
                await role.CreateAsync(new IdentityRole<Guid>("Admin"));
                await role.CreateAsync(new IdentityRole<Guid>("SuperAdmin"));
                await role.CreateAsync(new IdentityRole<Guid>("User"));
                await context.SaveChangesAsync();
            }
            
            if (!user.Users.Any())
            {
                var super = new ApplicationUser()
                {
                    Email = "mohamedaramadan130@gmail.com",
                    UserName="moafmed",
                    Fname = "mohamed",
                    Lname = "Ahmed",
                    PhoneNumber="01056620323",
                    Age = 23,
                    IsBlocked = false
                };
                var super2 = new ApplicationUser()
                {
                    Email = "mohamedaramadan@gmail.com",
                    UserName = "moahamed",
                    Fname = "mohamed",
                    PhoneNumber = "01057620323",
                    Lname = "AhmedAli",
                    Age = 25,
                    IsBlocked = false
                };

                await user.CreateAsync(super, "G7kLp9#Qw");

                await user.CreateAsync(super2, "G7r!v2$kLp9#Qw");
                await context.SaveChangesAsync();
            }
                
        }
    }
}
