using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Configuraitons.ReviewConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.BookReview)
            .WithMany(b => b.ReviewBook)
            .HasForeignKey(r => r.Bookid)
            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Users)
                .WithMany(b => b.reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Hotel)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(r => r.Room)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.RoomID)
            .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
