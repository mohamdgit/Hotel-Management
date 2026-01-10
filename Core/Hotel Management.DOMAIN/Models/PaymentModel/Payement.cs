using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.DOMAIN.Models.BookModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.PaymentModel
{
    public class Payement : BaseEntity<int>
    {
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [InverseProperty(nameof(Book.BookPayment))]
        public  Book PaymentBook { get; set; }
        public DateTime createdat { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentIntentId { get; set; } = null!;
        public string? ClientSecret { get; set; }
        public decimal Price { get; set; }
        public PaymentState PaymentState { get; set; }
        
    }
}
