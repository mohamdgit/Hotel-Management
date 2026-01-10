using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.PaymentDtos
{
    public class AddPaymentDto
    {
        public int BookId { get; set; }


        public DateTime createdat { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentIntentId { get; set; } = null!;
        public string? ClientSecret { get; set; }
        public decimal Price { get; set; }
    }
}
