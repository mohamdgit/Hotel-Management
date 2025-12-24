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
    public class HotelFeaturesConfiguration : IEntityTypeConfiguration<HotelFeatures>
    {
        public void Configure(EntityTypeBuilder<HotelFeatures> builder)
        {
            builder.HasOne(p => p.Feature)
                .WithMany(p => p.HotelFeatures)
                .HasForeignKey(p => p.FeatureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.HotelOfFeatures)
               .WithMany(h => h.Hotel_Features)
               .HasForeignKey(p => p.HotelId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
