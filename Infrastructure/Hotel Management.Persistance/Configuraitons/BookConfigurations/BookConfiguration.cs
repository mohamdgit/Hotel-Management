using Hotel_Management.DOMAIN.Models.BookModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Configuraitons.BookConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(p => p.RoomBooked).WithOne(p => p.BookOfTheRoom).HasForeignKey<Book>(p => p.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.User).WithMany(p => p.Books).HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.ReviewBook).WithOne(p => p.BookReview)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
