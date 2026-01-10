using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.PaymentModel;
using Hotel_Management.ServiceAbstraction.IserviceOfPayment;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.PaymentDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services
{
    public class PaymentService(IConfiguration configuration,IUow uow) : IPaymentService
    {
        public async Task<AddPaymentDto> AddPayemantIntent(int bookingId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            var paymentRepo= uow.GenerateRepo<Payement, int>();
            var bookRepo = uow.GenerateRepo<Book, int>();
            itemsQueryParam? param = new itemsQueryParam();
            param.BookId = bookingId;
            var spec = new BookigSpecification(param);
            var book =  bookRepo.GetAllSpecificationAsync(spec).FirstOrDefault();
            if (book == null)
                throw new Exception("Booking not found");

            if (book.Bookstate != BookState.Pending)
                throw new Exception("Booking is not payable");

            // حساب السعر
            decimal roomPrice = book.RoomBooked.Sum(r => r.PricePerNight);
            var nights = (book.Todate - book.Fromdate).Days;
            if (nights <= 0)
                throw new Exception("Invalid booking dates");

            var totalPrice = roomPrice * nights;

           
            if (book.BookPayment == null)
            {
                book.BookPayment = new Payement
                {
                    BookId = bookingId,
                    Price = totalPrice,
                    PaymentState = PaymentState.Pendding,
                    PaymentMethod = "card",
                    createdat = DateTime.UtcNow
                };

                await paymentRepo.Add(book.BookPayment);
                await uow.SaveChanges();
            }

           
            var paymentService = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(totalPrice * 100), 
                Currency = "gbp", 
                PaymentMethodTypes = new List<string> { "card" },
                Metadata = new Dictionary<string, string>
                {
                    ["BookingId"] = bookingId.ToString()
                }
            };

            var intent = await paymentService.CreateAsync(options);

            book.BookPayment.PaymentIntentId = intent.Id;
            book.BookPayment.ClientSecret = intent.ClientSecret;
            book.BookPayment.PaymentMethod = "card";

            await uow.SaveChanges();

            return new AddPaymentDto
            {
                BookId = bookingId,
                createdat = DateTime.UtcNow,
                ClientSecret = intent.ClientSecret,
                Price = (long)(totalPrice * 100),
                PaymentIntentId = intent.Id
            };
        }

    }
}
