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
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
builder.HasOne(p => p.Manager)
       .WithOne(p => p.ManagedHotel)
       .HasForeignKey<Hotel>(p => p.managerId)
       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
