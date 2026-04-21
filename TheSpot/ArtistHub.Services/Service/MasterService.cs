using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ArtistHub.Services.Service
{
    public class MasterService : IMasterService
    {
        private readonly IHttpContextAccessor _httpContext;

        private readonly IUOW _uow;
        public MasterService(IUOW _uow, IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
            this._uow = _uow;
        }
        public async ValueTask<ApiResponse<bool>> CreateCategory(CategoryMaster model)
        {
            try
            {
                //var role = Nameidentifier.GetRole(_httpContext.HttpContext);
                //var userId = Nameidentifier.GetUserId(_httpContext.HttpContext);
                //if (userId != null || string.IsNullOrEmpty(role))
                //    return new ApiResponse<bool>("Unauthorized", false, ResponseMessage.Error, false);

                int id = model.CategoryId;
                if (model.CategoryId != 0)
                {
                    var existing = (await this._uow.GenricRepository<CategoryMaster>().GetAllAsync(filter: x => x.CategoryId == model.CategoryId)).FirstOrDefault();
                    if (existing != null)
                    {
                        existing.CategoryName = model.CategoryName;
                        this._uow.GenricRepository<CategoryMaster>().Update(existing);
                    }
                }
                var exists = (await this._uow.GenricRepository<CategoryMaster>().GetAllAsync(x => x.CategoryName == model.CategoryName));
                if (exists.Any())
                {
                    return new ApiResponse<bool>(Message.AlreadyExist, false, ResponseMessage.Error, false);
                }
                model.CreatedBy = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                await this._uow.GenricRepository<CategoryMaster>().AddAsync(model);
                await this._uow.SaveAsync();
                return new ApiResponse<bool>(id == 0 ? Message.Saved : Message.Updated, true, ResponseMessage.Ok, true);

            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }


        public async ValueTask<ApiResponse<bool>> DeleteCategory(int id)
        {
            try
            {
                var response = (await this._uow.GenricRepository<CategoryMaster>().GetAllAsync(x => x.CategoryId == id)).FirstOrDefault();
                if (response != null)
                {
                    response.IsDeleted = true;
                    this._uow.GenricRepository<CategoryMaster>().Update(response);
                    this._uow.Save();
                }
                return new ApiResponse<bool>(Message.Deleted, true, ResponseMessage.Ok, true);
            }
            catch (Exception Message)
            {
                return new ApiResponse<bool>(Message.Message, false, ResponseMessage.Error, false);
            }
        }

        public async ValueTask<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategory()
        {
            var respoonse = await this._uow.GenricRepository<CategoryMaster>().GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            var data = respoonse.Select(x => new CategoryDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted
            }).ToList();
            return new ApiResponse<IEnumerable<CategoryDto>>(Message.Retrieved, true, ResponseMessage.Ok, data);
        }

        public async ValueTask<ApiResponse<IEnumerable<TblRoleMaster>>> GetAllRoles()
        {
            var response = await this._uow.GenricRepository<TblRoleMaster>().GetAllAsync(x => x.IsActive == true&&x.RoleId!=1);
            return response.Any() ? new ApiResponse<IEnumerable<TblRoleMaster>>(Message.Retrieved, true, ResponseMessage.Ok, response) :
                new ApiResponse<IEnumerable<TblRoleMaster>>(Message.NoRecord, false, ResponseMessage.NoRecords, Enumerable.Empty<TblRoleMaster>());
        }
        public async ValueTask<ApiResponse<bool>> CreateRoles(TblRoleMaster model)
        {
            var role = Nameidentifier.GetRole(_httpContext.HttpContext);
            var userId = Nameidentifier.GetUserId(_httpContext.HttpContext);
            //if (userId != 0 || string.IsNullOrEmpty(role))
            //    return new ApiResponse<bool>("Unauthorized", false, ResponseMessage.Error, false);
            int id = model.RoleId;
            if (id != 0)
            {
                var existing = this._uow.GenricRepository<TblRoleMaster>().GetAllAsync(x => x.IsActive == true && x.RoleId == model.RoleId).Result.FirstOrDefault();
                if (existing != null)
                {
                    existing.RoleName = model.RoleName;
                    existing.Description = model.Description;
                    this._uow.GenricRepository<TblRoleMaster>().Update(existing);
                }
            }
            await this._uow.GenricRepository<TblRoleMaster>().AddAsync(model);
            this._uow.Save();
            return new ApiResponse<bool>(id == 0 ? Message.Saved : Message.Updated, true, ResponseMessage.Ok, true);
        }
    }
}
