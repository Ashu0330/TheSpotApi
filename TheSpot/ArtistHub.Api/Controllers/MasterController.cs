using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using ArtistHub.Services.Service;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtistHub.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize(Roles = RoleMaster.Admin)]
    public class MasterController : ControllerBase
    {

        private readonly IMasterService service;
        private readonly IHttpContextAccessor _httpContext;

        public MasterController(IMasterService service, IHttpContextAccessor _httpContext)
        {
            this._httpContext = _httpContext;
            this.service = service;
        }
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> CreateCategory(CategoryMaster model)
        {
            //var userId = Nameidentifier.GetUserId(_httpContext.HttpContext);
            

            //if (userId !=0)
            //    return new ApiResponse<bool>("Unauthorized", false, ResponseMessage.Error, false);

            return await this.service.CreateCategory(model);
        }
    
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> DeleteCategory(int id) => await this.service.DeleteCategory(id);
        [AllowAnonymous]
        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategory() => await this.service.GetAllCategory();
        [HttpPost]
        public async ValueTask<ApiResponse<bool>> CreateRoles(TblRoleMaster model) => await this.service.CreateRoles(model);
        [AllowAnonymous]
        [HttpGet]
        public async ValueTask<ApiResponse<IEnumerable<TblRoleMaster>>> GetAllRoles() => await this.service.GetAllRoles();
    }
}
