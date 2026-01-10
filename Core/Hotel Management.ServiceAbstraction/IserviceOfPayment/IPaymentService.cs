using Hotel_Management.Shared.DTOs.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfPayment
{
    public interface IPaymentService
    {
        public Task<AddPaymentDto> AddPayemantIntent(int bokkingid);
    }
}
