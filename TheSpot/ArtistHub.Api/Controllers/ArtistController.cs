using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize(Roles = RoleMaster.Artist)]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService Service;
        private readonly ImageHelper imageHelper;

        public ArtistController(IArtistService Service, ImageHelper imageHelper)
        {
            this.Service = Service;
            this.imageHelper = imageHelper;
        }

        //[HttpGet]
        //public async ValueTask<ApiResponse<TblArtist>> GetArtistByUserId(int userId) => await this.Service.GetArtistByUserId(userId);
        [AllowAnonymous]

        [HttpPost]
        public async ValueTask<ApiResponse<bool>> CreateArtist(TblArtist model) => await this.Service.CreateArtist(model);

        [HttpPost]

        public async Task<ApiResponse<bool>> ArtistMedia([FromForm] ArtistMediaDto models)
        {
            return await this.Service.ArtistMedia(models);
        }
        //[AllowAnonymous]
        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<TblArtistMedium>>> GetArtistMedia(int artistId)=>await this.Service.GetArtistMedia(artistId);

        [HttpPost]
        public async ValueTask<ApiResponse<IEnumerable<TblBooking>>> GetAllBooking(filterModel model) => await this.Service.GetAllBooking(model);

        [HttpGet]
        public async ValueTask<ApiResponse<bool>> AcceptRejectBooking(int bookingId, string status) => await this.Service.AcceptRejectBooking(bookingId, status);
        [AllowAnonymous]
        [HttpGet]
        public async ValueTask<ApiResponse<ArtistDashboardDto>> ArtistDashboard() => await this.Service.ArtistDashboard();


    }
}
