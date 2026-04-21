using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Repository.RepositoryServices;
using ArtistHub.Services.IService;
using KFPLSkillUp.Presentation.Helper;



namespace ArtistHub.Services.Service
{
    public class UserService : IUserService
    {
        #region Services
        private readonly IRepositoryService<TblUser> service;
        private readonly IUOW uOW;
        private readonly JwtHelper _jwt;
        #endregion

        #region Constructor
        public UserService(IRepositoryService<TblUser> service, IUOW uOW, JwtHelper jwt)
        {
            this.service = service;
            this.uOW = uOW;
            _jwt = jwt;
        }
        #endregion

        #region UserRegion

        public async ValueTask<ApiResponse<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var result = (await this.uOW.GenricRepository<TblUser>().GetAllAsync()).Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    RoleName = (this.uOW.GenricRepository<TblRoleMaster>().GetAllAsync(x=>x.RoleId == u.RoleId)).Result.FirstOrDefault()?.RoleName,
                    City = u.City,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                }).ToList();

                return result.Any() ? new ApiResponse<IEnumerable<UserDto>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                    new ApiResponse<IEnumerable<UserDto>>(Message.NoRecord, false, ResponseMessage.Error, Enumerable.Empty<UserDto>());
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<UserDto>>(ex.Message, false, ResponseMessage.Error, Enumerable.Empty<UserDto>());
            }
        }

        public async ValueTask<ApiResponse<UserDto>> GetUsersById(long id)
        {
            try
            {
                var user = await this.uOW.GenricRepository<TblUser>().GetAllAsync(x => x.UserId == id);
                var userData = user.FirstOrDefault();

                if (user == null)
                {
                    return new ApiResponse<UserDto>(Message.NotExist, false, ResponseMessage.NotFound, new UserDto());
                }
                var role = this.uOW.GenricRepository<TblRoleMaster>().GetAllAsync(x => x.RoleId == userData.RoleId).Result.FirstOrDefault()?.RoleName ?? "";
                var result = new UserDto
                {
                    UserId = userData.UserId,
                    FullName = userData.FullName,
                    Email = userData.Email,
                    Phone = userData.Phone,
                    RoleName = role,
                    City = userData.City,
                    IsActive = userData.IsActive,
                    CreatedAt = userData.CreatedAt,
                    UpdatedAt = userData.UpdatedAt
                };
                return result != null ? new ApiResponse<UserDto>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                    new ApiResponse<UserDto>(Message.NoRecord, false, ResponseMessage.Error, new UserDto());
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDto>(ex.Message, false, ResponseMessage.Error, new UserDto());
            }
        }

        #endregion
    }
}
