using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize(Roles = "Lounge")]
    [ApiController]
    public class LoungeController : ControllerBase
    {
        private readonly ILoungeService service;
        public LoungeController(ILoungeService service)
        {
            this.service = service;
        }
        [AllowAnonymous]
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> CreateLongue(TblLounge model) => await this.service.CreateLongue(model);
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> CreateEvent(TblEvent model) => await this.service.CreateEvent(model);

        [HttpPost]
        public async ValueTask<ApiResponse<IEnumerable<TblEvent>>> GetAllEvent(filterModel model) => await this.service.GetAllEvent(model);
    }
}
