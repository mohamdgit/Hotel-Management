using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.PaymentModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Contexts.HotelContext
{
    public class HotelContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public HotelContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HotelFeatures>().HasKey(p => new { p.FeatureId, p.HotelId });
            builder.ApplyConfigurationsFromAssembly(typeof(HotelContext).Assembly);
        }
        public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<HotelPhotos> HotelPhotos { get; set; } = null!;

        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<HotelFeatures> HotelFeatures { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomType> RoomsTypes { get; set; } = null!;

        public DbSet<Payement> Payements { get; set; } = null!;

        public DbSet<RoomPhoto> RoomPhotoss { get; set; } = null!;
        




    }
}
