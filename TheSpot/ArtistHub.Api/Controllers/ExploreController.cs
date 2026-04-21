using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArtistHub.Presentation.Domain.ArtistExploreFilterModel;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ExploreController : ControllerBase
    {
        private readonly IExploreService Service;
        public ExploreController( IExploreService service)
        {
                this.Service = service;
        }
        [HttpGet]
        public async ValueTask<ApiResponse<TblArtist>> GetArtistByUserId(long userId) => await this.Service.GetArtistByUserId(userId);
        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<object>>> GetAllArtist() => await this.Service.GetAllArtist();
        [HttpPost]
        public async ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetArtistByCategory(ArtistExploreFilterModel model) => await this.Service.GetArtistByCategory(model);
        [HttpPost]
        public async ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetLoungeByCategory(LoungeExploreFilterModel model) => await this.Service.GetLoungeByCategory(model);
        [HttpPost]
        public async ValueTask<ApiResponse<IEnumerable<EventDto>>> GetEventByCategory(EventExploreFilterModel model) => await this.Service.GetEventByCategory(model);
    }
}
