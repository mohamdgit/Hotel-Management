
using Hotel_Management.Customized_Exceptions;
using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Contracts.Seeding;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.Persistance.HotelFeaturesRepo;
using Hotel_Management.Persistance.Seeding;
using Hotel_Management.Persistance.UOW;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.ServiceImplementiton.AutoMapping;
using Hotel_Management.ServiceImplementiton.Services.HotelService;
using Hotel_Management.ServiceImplementiton.Services.ServiceManager;
using Hotel_Management.Shared.ErrorToReturn;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hotel_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<HotelContext>
             (o => o.UseSqlServer(builder.Configuration.GetConnectionString("HotelDb")));
            builder.Services.AddScoped<IUow, UOW>();
            builder.Services.AddScoped<IHotelFeatureReposatory, HotelFeaturesReposatory>();
            builder.Services.AddScoped<IServiceManager, ServiceMnager>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new HotelProfile());
                cfg.AddProfile(new ReviewProfile());



            });
            builder.Services.AddIdentity<ApplicationUser,IdentityRole<Guid>>()
                .AddEntityFrameworkStores<HotelContext>().AddDefaultTokenProviders();
        

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["jwtauth:Issuer"],
                    ValidAudience = builder.Configuration["jwtauth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["jwtauth:securitykey"])
                    )
                };
            });
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            Options.InvalidModelStateResponseFactory =  (context) =>
            {
                var errors = context.ModelState.Where(m => m.Value.Errors.Any())
                .Select(p => new ValidationError()
                {
                    feild = p.Key,
                    error = p.Value.Errors.Select(p => p.ErrorMessage)
                });
                var response = new ValidationErrorToReturn()
                {
                    ValidationError = errors
                };
                return new BadRequestObjectResult(response);

            }

            );
            builder.Services.AddScoped<ISeeding, Seeding>();

            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            using (var scope = app.Services.CreateScope())
            {
                var seedingService = scope.ServiceProvider.GetRequiredService<ISeeding>();
                seedingService.SeedUserRoles().GetAwaiter().GetResult();
            }
           // app.UseMiddleware<ManageExceptionMiddlWare>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
