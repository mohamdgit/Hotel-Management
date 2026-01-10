using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.ApplicationUserModel;
using Hotel_Management.ServiceAbstraction.IBookingService;
using Hotel_Management.ServiceAbstraction.IRoomTypeService;
using Hotel_Management.ServiceAbstraction.IserviceOfAuthentication;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceAbstraction.IserviceOfPayment;
using Hotel_Management.ServiceAbstraction.RoomService;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.ServiceImplementiton.Services.HotelService;
using Hotel_Management.ServiceImplementiton.Services.ServiceOfAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.ServiceManager
{
    public class ServiceMnager(UserManager<ApplicationUser> user, RoleManager<IdentityRole<Guid>> role, IConfiguration configure,IUow uow,IMapper mapper, IHotelFeatureReposatory hotelrepo, IHttpContextAccessor httpContext) : IServiceManager

    {

        private readonly Lazy<IAuthenticatonService> Authentication = new Lazy<IAuthenticatonService>(() => new AuthenticationService(user, role, configure));
        public IAuthenticatonService serviceOfAuthentication => Authentication.Value;
        private readonly Lazy<IHotelFeatureService> HotelFeatureService = new Lazy<IHotelFeatureService>(() => new HotelFeatureservice(uow, hotelrepo,mapper));
        public IHotelFeatureService serviceOfHotelFeature => HotelFeatureService.Value;
        private readonly Lazy<IFeatureService> FeaturesService = new Lazy<IFeatureService>(() => new FeaturesService(uow, mapper));
        public IFeatureService serviceOfFeatures => FeaturesService.Value;

        private readonly Lazy<IHotelService> HotelService = new Lazy<IHotelService>(() => new HotelService.HotelService(uow, mapper));
        public IHotelService serviceOfHotel => HotelService.Value;

        private readonly Lazy<IReviewService> ReviewsService = new Lazy<IReviewService>(() => new ReviewService.ReviewService(uow, mapper));
        public IReviewService ReviewService => ReviewsService.Value;

        private readonly Lazy<IPhotoService> PhotoServicelazy = new Lazy<IPhotoService>(() => new PhotoService(uow , mapper , httpContext));
        public IPhotoService PhotoService => PhotoServicelazy.Value;

        private readonly Lazy<IRoomTypeService> RoomTypeServicelazy = new Lazy<IRoomTypeService>(() => new RoomTypeService.RoomTypeService(uow, mapper));
        public IRoomTypeService RoomTypeService => RoomTypeServicelazy.Value;
        private readonly Lazy<IPhotoRoomService> PhotoRoomServicelazy = new Lazy<IPhotoRoomService>(() => new PhotoRoomService(uow, mapper, httpContext));
        public IPhotoRoomService PhotoRoomService => PhotoRoomServicelazy.Value;
        private readonly Lazy<IRoomService> RoomServicelazy = new Lazy<IRoomService>(() => new RoomService.RoomService(uow, mapper, httpContext));
        public IRoomService RoomService => RoomServicelazy.Value;

        private readonly Lazy<IBookingService> BookServicelazy = new Lazy<IBookingService>(() => new BookingService.BookingService(uow, mapper));
        public IBookingService BookService => BookServicelazy.Value;
        private readonly Lazy<IPaymentService> PaymentServicelazy = new Lazy<IPaymentService>(() => new PaymentService(configure, uow));

        public IPaymentService PaymentServices =>  PaymentServicelazy.Value;
    }
}
