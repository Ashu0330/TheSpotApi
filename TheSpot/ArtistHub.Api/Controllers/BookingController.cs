using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize(Roles = RoleMaster.UserOrLounge)]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService service;
        public BookingController(IBookingService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> BookArtist(TblBooking model) => await this.service.BookArtist(model);
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> UpdateBooking(TblBooking model) => await this.service.UpdateBooking(model);
        [HttpGet]
        public async ValueTask<ApiResponse<bool>> CancelBooking(int bookingId) => await this.service.CancelBooking(bookingId);

    }
}
