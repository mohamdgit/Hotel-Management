using Hotel_Management.DOMAIN.Models.HotelModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Configuraitons.HotelConfigurations
{
    public class HotelPhotosConfiguration : IEntityTypeConfiguration<HotelPhotos>
    {
        public void Configure(EntityTypeBuilder<HotelPhotos> builder)
        {
            builder.HasOne(p => p.Hotelsofphotos).WithMany(p => p.Photos).HasForeignKey(p => p.HotelId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
