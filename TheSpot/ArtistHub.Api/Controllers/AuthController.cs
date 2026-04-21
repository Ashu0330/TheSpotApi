using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using ArtistHub.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService _authService)
        {
            this._authService = _authService;
        }

        [HttpPost]
        public async ValueTask<ApiResponse<UserDto>> LoginUser(LoginRequestDto model) => await this._authService.LoginUser(model);

        [HttpPost]
        public async ValueTask<ApiResponse<long>> RegisterUser(TblUser model) => await this._authService.RegisterUser(model);
    }
}
