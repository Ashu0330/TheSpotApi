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
    [ApiController]
    [Authorize(Roles = RoleMaster.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService _adminService)
        {
            this._adminService = _adminService;
        }
        [HttpGet]
        public async ValueTask<ApiResponse<bool>> ApproveArtist(int id) =>
            await this._adminService.ApproveArtist(id);
        [HttpGet]
        public async ValueTask<ApiResponse<bool>> ApproveEvent(int id) =>
            await this._adminService.ApproveEvent(id);
        [HttpGet]
        public async ValueTask<ApiResponse<bool>> ApproveLounge(int id) =>
            await this._adminService.ApproveLounge(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> DeleteArtist(int id) =>
            this._adminService.DeleteArtist(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> DeleteEvent(int id) =>
            this._adminService.DeleteEvent(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> DeleteLounge(int id) =>
            this._adminService.DeleteLounge(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> InActiveLounge(int id) =>
            this._adminService.InActiveLounge(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> InActiveArtist(int id) =>
            this._adminService.InActiveArtist(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> RejectEvent(int id) =>
                    this._adminService.RejectEvent(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> RejectLounge(int id) =>
                    this._adminService.RejectLounge(id);
        [HttpGet]
        public ValueTask<ApiResponse<bool>> RejectArtist(int id) =>
                this._adminService.RejectArtist(id);
        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetAllArtistApproval() =>
            await this._adminService.GetAllArtistApproval();

        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetAllLoungeApproval() =>
            await this._adminService.GetAllLoungeApproval();
    }
}
