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
    public class RoomPhotoConfiguration : IEntityTypeConfiguration<RoomPhoto>
    {
        public void Configure(EntityTypeBuilder<RoomPhoto> builder)
        {
            builder.HasOne(p => p.Room).WithMany(p => p.Photos).HasForeignKey(p => p.RoomId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
