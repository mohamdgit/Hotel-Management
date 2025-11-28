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
            builder.HasOne(p => p.HotelofRoom).WithMany(p => p.HotelRooms).HasForeignKey(p => p.Hotelid).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
