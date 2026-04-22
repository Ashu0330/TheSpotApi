using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Dapper;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static ArtistHub.Presentation.Helper.Utilities;

namespace ArtistHub.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUOW uOW;
        private readonly JwtHelper _jwt;



        public AuthService(IUOW uOW, JwtHelper _jwt)
        {
            this.uOW = uOW;
            this._jwt = _jwt;
        }

        public async ValueTask<ApiResponse<long>> RegisterUser(TblUser model)
        {
            try
            {
                if (model.RoleId == (int)RoleEnum.Admin || model.FullName == RoleMaster.Admin)
                {
                    return new ApiResponse<long>(Message.Invalid, false, ResponseMessage.Error, 0);
                }

                if (model.UserId != 0)
                {
                    var response = (await this.uOW.GenricRepository<TblUser>().GetAllAsync(x => x.UserId == model.UserId)).FirstOrDefault();
                    if (response != null)
                    {
                        var existing = (await uOW.GenricRepository<TblUser>().GetAllAsync(x => x.Email == model.Email || x.Phone == model.Phone)).FirstOrDefault();

                        if (existing != null)
                            return new ApiResponse<long>("Email already exists", false, ResponseMessage.Error, model.UserId);
                        response.FullName = model.FullName;
                        response.Email = model.Email;
                        response.Phone = model.Phone;
                        response.RoleId = model.RoleId;
                        response.City = model.City;
                        response.UpdatedAt = model.UpdatedAt;
                        this.uOW.GenricRepository<TblUser>().Update(response);
                    }
                }
                else
                {
                    var existing = (await uOW.GenricRepository<TblUser>().GetAllAsync(x => x.Email == model.Email || x.Phone == model.Phone)).FirstOrDefault();
                    model.PasswordHash = PasswordHasher.HashPassword(model.PasswordHash);
                    await this.uOW.GenricRepository<TblUser>().AddAsync(model);
                }
                await this.uOW.SaveAsync();
                return new ApiResponse<long>(model.UserId != 0 ? Message.Saved : Message.Updated, true, ResponseMessage.Ok, model.UserId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<long>(ex.Message, false, ResponseMessage.Error, 0);
            }
        }
        public async ValueTask<ApiResponse<UserDto>> LoginUser(LoginRequestDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return new ApiResponse<UserDto>("Username and password are required", false, ResponseMessage.Error, null);
                }
                var user = (await this.uOW.GenricRepository<TblUser>().GetAllAsync(u => u.Email == model.Username)).FirstOrDefault();
                if (user == null)
                {
                    return new ApiResponse<UserDto>("Invalid Email", false, ResponseMessage.Error, null);
                }
                var isValid = PasswordHasher.VerifyPassword(model.Password, user.PasswordHash);
                if (!isValid)
                {
                    return new ApiResponse<UserDto>("Invalid Password", false, ResponseMessage.Error, null);
                }
                var role = (await this.uOW.GenricRepository<TblRoleMaster>().GetAllAsync(r => r.RoleId == user.RoleId)).FirstOrDefault() ?? new TblRoleMaster();
                var artist = (await this.uOW.GenricRepository<TblArtist>().GetAllAsync(a => a.UserId == user.UserId)).FirstOrDefault();
                var media = (await this.uOW.GenricRepository<TblArtistMedium>().GetAllAsync(m => m.ArtistId == artist.ArtistId)).FirstOrDefault();
                var category = (await this.uOW.GenricRepository<CategoryMaster>().GetAllAsync(c => c.RoleId == role.RoleId)).FirstOrDefault() ?? new CategoryMaster();
                var token = _jwt.GenerateToken(user.UserId, user.Email, role.RoleName);
                var result = new UserDto
                {
                    Token = token,
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    RoleName = role.RoleName,
                    RoleId = user.RoleId,
                    City = user.City,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    ProfileImage = media?.MediaCategory == "Profile" ? media.FileUrl : null

                };
                return result != null ? new ApiResponse<UserDto>(Message.LoggedIn, true, ResponseMessage.Ok, result) :
                new ApiResponse<UserDto>(Message.Invalid, false, ResponseMessage.Error, new UserDto());
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDto>(ex.Message, false, ResponseMessage.Error, new UserDto());
            }
        }


    }
}
