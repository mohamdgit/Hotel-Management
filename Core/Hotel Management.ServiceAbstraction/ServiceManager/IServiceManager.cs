using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceAbstraction.IserviceOfPayment;
using Hotel_Management.ServiceAbstraction.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.ServiceManager
{
    public interface IServiceManager
    {
        public IserviceOfAuthentication.IAuthenticatonService serviceOfAuthentication { get;  }
        public IHotelService serviceOfHotel { get; }
        public IFeatureService serviceOfFeatures { get; }
        public IHotelFeatureService serviceOfHotelFeature { get; }
        public IReviewService ReviewService { get; }
        public IPhotoService PhotoService { get; }
        public IPhotoRoomService PhotoRoomService { get; }

        public IBookingService.IBookingService BookService { get; }

        public IRoomService RoomService { get; }
        public IRoomTypeService.IRoomTypeService RoomTypeService { get; }
       public IPaymentService PaymentServices { get; }

    }
}
