using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using ArtistHub.Services.Service;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    //[ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        [Authorize]
        [HttpGet]
        public async Task<ApiResponse<UserDto>> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<UserDto>("Unauthorized", false, ResponseMessage.Error, null);

            return await userService.GetUsersById(long.Parse(userId));
        }
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        #region Users


        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<UserDto>>> GetUsers() => await this.userService.GetUsers();
        [HttpGet]
        public async ValueTask<ApiResponse<UserDto>> GetUsersById(long id) => await this.userService.GetUsersById(id);

        #endregion
    }
}
