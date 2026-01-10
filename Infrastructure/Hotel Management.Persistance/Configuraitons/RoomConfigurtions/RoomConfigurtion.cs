using Hotel_Management.DOMAIN.Models.RoomModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Configuraitons.RoomConfigurtions
{
    public class RoomConfigurtion : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasOne(r => r.Hotel)
                   .WithMany(h => h.HotelRooms)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.NoAction);

            
            builder.HasOne(r => r.RoomType)
                   .WithMany(rt => rt.Rooms)
                   .HasForeignKey(r => r.RoomTypeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
