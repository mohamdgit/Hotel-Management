using Hotel_Management.Customized_Exceptions;
using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Contracts.Seeding;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.Persistance.HotelFeaturesRepo;
using Hotel_Management.Persistance.Seeding;
using Hotel_Management.Persistance.UOW;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.ServiceImplementiton.AutoMapping;
using Hotel_Management.ServiceImplementiton.Services.ServiceManager;
using Hotel_Management.Shared.ErrorToReturn;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Services --------------------
builder.Services.AddControllers();
builder.Services.AddDbContext<HotelContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("HotelDb"))
);

builder.Services.AddScoped<IUow, UOW>();
builder.Services.AddScoped<IHotelFeatureReposatory, HotelFeaturesReposatory>();
builder.Services.AddScoped<IServiceManager, ServiceMnager>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new HotelProfile());
    cfg.AddProfile(new ReviewProfile());

});

// Identity + JWT
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<HotelContext>()
    .AddDefaultTokenProviders();

var key = Encoding.UTF8.GetBytes(builder.Configuration["jwtauth:securitykey"]);
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
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
         policy.RequireClaim(ClaimTypes.Role, "Admin"));

    options.AddPolicy("User", policy =>
         policy.RequireClaim(ClaimTypes.Role, "User"));

    options.AddPolicy("SuperAdmin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "SuperAdmin"));

    options.AddPolicy("AdminOrSuperAdmin", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.IsInRole("SuperAdmin")
       ));
});


// Seeding & Helpers
builder.Services.AddScoped<ISeeding, Seeding>();
builder.Services.AddScoped<ImageUrlResolver>();
builder.Services.AddScoped<ImageRoomUrlResolver>();

// Custom validation response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(m => m.Value.Errors.Any())
            .Select(p => new ValidationError
            {
                feild = p.Key,
                error = p.Value.Errors.Select(e => e.ErrorMessage)
            });

        var response = new ValidationErrorToReturn
        {
            ValidationError = errors
        };

        return new BadRequestObjectResult(response);
    };
});



// -------------------- Build --------------------
var app = builder.Build();


// Seeding users & roles
using (var scope = app.Services.CreateScope())
{
    var seedingService = scope.ServiceProvider.GetRequiredService<ISeeding>();
    seedingService.SeedUserRoles().GetAwaiter().GetResult();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
