using Hotel_Management.DOMAIN.Models.PaymentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.Configuraitons.PaymentConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payement>
    {
        public void Configure(EntityTypeBuilder<Payement> builder)
        {
            builder.HasOne(p => p.PaymentBook).WithOne(p => p.BookPayment).HasForeignKey<Payement>(p => p.BookId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
