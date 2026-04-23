using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using Dapper;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Http;
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
        private readonly ImageHelper imageHelper;



        public AuthService(IUOW uOW, JwtHelper _jwt, ImageHelper imageHelper)
        {
            this.uOW = uOW;
            this._jwt = _jwt;
            this.imageHelper = imageHelper;
        }
        public async ValueTask<ApiResponse<long>> RegisterUser(UserDto model)
        {
            try
            {
                if (model.RoleId == (int)RoleEnum.Admin)
                {
                    return new ApiResponse<long>(Message.Invalid, false, ResponseMessage.Error, 0);
                }

                // 👉 CREATE
                if (model.UserId == 0)
                {
                    var existing = (await uOW.GenricRepository<TblUser>()
                        .GetAllAsync(x => x.Email == model.Email || x.Phone == model.Phone))
                        .FirstOrDefault();

                    if (existing != null)
                        return new ApiResponse<long>("Email or Phone already exists", false, ResponseMessage.Error, 0);

                    var entity = new TblUser
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        RoleId = model.RoleId,
                        City = model.City,
                        PasswordHash = PasswordHasher.HashPassword(model?.PasswordHash ?? string.Empty), // ✅ FIXED
                        CreatedAt = DateTime.UtcNow,
                        ProfileImage = imageHelper.UploadLowQualityImage(
                            ImageDirectories.ProfileImages, model?.Profilefile)
                    };

                    await uOW.GenricRepository<TblUser>().AddAsync(entity);
                    await uOW.SaveAsync();

                    return new ApiResponse<long>(Message.Saved, true, ResponseMessage.Ok, entity.UserId);
                }

                // 👉 UPDATE
                else
                {
                    var user = (await uOW.GenricRepository<TblUser>()
                        .GetAllAsync(x => x.UserId == model.UserId))
                        .FirstOrDefault();

                    if (user == null)
                        return new ApiResponse<long>("User not found", false, ResponseMessage.Error, 0);

                    var existing = (await uOW.GenricRepository<TblUser>()
                        .GetAllAsync(x => (x.Email == model.Email || x.Phone == model.Phone)
                                          && x.UserId != model.UserId))
                        .FirstOrDefault();

                    if (existing != null)
                        return new ApiResponse<long>("Email or Phone already exists", false, ResponseMessage.Error, model.UserId);

                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.Phone = model.Phone;
                    user.RoleId = model.RoleId;
                    user.City = model.City;
                    user.UpdatedAt = DateTime.UtcNow;

                    if (model?.Profilefile != null)
                    {
                        user.ProfileImage = imageHelper.UploadLowQualityImage(
                            ImageDirectories.ProfileImages, model.Profilefile);
                    }

                    uOW.GenricRepository<TblUser>().Update(user);
                    await uOW.SaveAsync();

                    return new ApiResponse<long>(Message.Updated, true, ResponseMessage.Ok, user.UserId);
                }
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
                    ProfileImage = ImageEnvironment.Baseurl + user.ProfileImage

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
