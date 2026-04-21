using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Repository.RepositoryServices;
using ArtistHub.Services.IService;
using Azure;
using Dapper;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ArtistHub.Services.Service
{
    public class ArtistService : IArtistService
    {
        private readonly IUOW uOW;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ImageHelper imageHelper;
        private readonly IRepositoryService<ArtistDashboardDto> repository;




        public ArtistService(IUOW uOW, IHttpContextAccessor _httpContext, ImageHelper imageHelper, IRepositoryService<ArtistDashboardDto> repository)
        {
            this.uOW = uOW;
            this._httpContext = _httpContext;
            this.imageHelper = imageHelper;
            this.repository = repository;

        }
        public async ValueTask<ApiResponse<bool>> CreateArtist(TblArtist model)
        {
            try
            {
                if (model.ArtistId != 0)
                {
                    var response = (await this.uOW.GenricRepository<TblArtist>().GetAllAsync(x => x.ArtistId == model.ArtistId)).FirstOrDefault();
                    if (response != null)
                    {
                        var userid = Nameidentifier.GetUserId(_httpContext.HttpContext);
                        response.ArtistId = model.ArtistId;
                        response.UserId = (long)userid;
                        response.Bio = model.Bio;
                        response.PricePerShow = model.PricePerShow;
                        response.Rating = model.Rating;
                        response.IsVerified = false;
                        response.CategoryId = model.CategoryId;
                        response.Spotify = model.Spotify;
                        response.Instagram = model.Instagram;
                        response.YouTube = model.YouTube;
                    }
                }
                else
                {
                    model.IsVerified = false;
                    model.IsDeleted = false;
                    model.IsActive = true;
                    await this.uOW.GenricRepository<TblArtist>().AddAsync(model);
                    var user = await this.uOW.GenricRepository<TblArtist>().GetAllAsync();


                }
                await this.uOW.SaveAsync();
                return new ApiResponse<bool>(Message.Saved, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }

        public async ValueTask<ApiResponse<bool>> ArtistMedia(ArtistMediaDto models)
        {
            if (models.ProfileImage != null)
            {
                var profilePath = this.imageHelper.UploadHighQualityImage(
                    ImageDirectories.ArtistImages,
                    models.ProfileImage);

                await this.uOW.GenricRepository<TblArtistMedium>().AddAsync(
                    new TblArtistMedium
                    {
                        ArtistId = models.ArtistId,
                        MediaCategory = "Profile",
                        FileUrl = profilePath,
                        Title = "Profile Image",
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    });
            }

            if (models.BannerImage != null)
            {
                var bannerPath = this.imageHelper.UploadHighQualityImage(
                    ImageDirectories.ArtistImages,
                    models.BannerImage);

                await this.uOW.GenricRepository<TblArtistMedium>().AddAsync(
                    new TblArtistMedium
                    {
                        ArtistId = models.ArtistId,
                        MediaCategory = "Banner",
                        FileUrl = bannerPath,
                        Title = "Banner Image",
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    });
            }

            if (models.ConcertImages != null && models.ConcertImages.Any())
            {
                int order = 1;

                foreach (var image in models.ConcertImages)
                {
                    var concertPath = this.imageHelper.UploadMidQualityImage(
                        ImageDirectories.ArtistImages,
                        image);

                    await this.uOW.GenricRepository<TblArtistMedium>().AddAsync(
                        new TblArtistMedium
                        {
                            ArtistId = models.ArtistId,
                            MediaCategory = "Concert",
                            FileUrl = concertPath,
                            Title = "Concert Image",
                            DisplayOrder = order++,
                            CreatedAt = DateTime.UtcNow,
                            IsDeleted = false
                        });
                }
            }

            await this.uOW.SaveAsync();

            return new ApiResponse<bool>(Message.Saved, true, ResponseMessage.Ok, true);
        }
      public async ValueTask<ApiResponse<IEnumerable<TblArtistMedium>>> GetArtistMedia(int artistId)
        {
            try
            {
                long userid = Nameidentifier.GetUserId(_httpContext.HttpContext);
                var result = await this.uOW.GenricRepository<TblArtistMedium>().GetAllAsync(filter:x=>x.ArtistId==userid);
                if (result.Any())
                {
                   var data = result.Select(x =>
                    {
                        x.Title = x.Title;
                        x.FileUrl= $"{ImageEnvironment.Baseurl}/{x.FileUrl}";
                        return x;
                    }).ToList();
                    return new ApiResponse<IEnumerable<TblArtistMedium>>(Message.Retrieved, true, ResponseMessage.Ok, data) ;
                }

                  return  new ApiResponse<IEnumerable<TblArtistMedium>>(Message.NoRecord, false, ResponseMessage.Error, new List<TblArtistMedium>());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<TblBooking>>> GetAllBooking(filterModel model)
        {
            try
            {
                var role = Nameidentifier.GetRole(_httpContext.HttpContext);
                var userId = Nameidentifier.GetUserId(_httpContext.HttpContext);
                int id = Convert.ToInt32(userId);
                IEnumerable<TblBooking> response;
                if (role == "Artist")
                {
                    response = await uOW.GenricRepository<TblBooking>()
                        .GetAllAsync(x =>
                            x.ArtistId == id &&
                            (string.IsNullOrEmpty(model.Status) || x.Status == model.Status));
                }
                else
                {
                    response = await uOW.GenricRepository<TblBooking>()
                        .GetAllAsync(x =>
                            x.BookedByUserId == id &&
                            (string.IsNullOrEmpty(model.Status) || x.Status == model.Status));
                }
                return response.Any() ? new ApiResponse<IEnumerable<TblBooking>>(Message.Retrieved, true, ResponseMessage.Ok, response) :
                    new ApiResponse<IEnumerable<TblBooking>>(Message.NoRecord, false, ResponseMessage.Error, Enumerable.Empty<TblBooking>());
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<TblBooking>>(ex.Message, false, ResponseMessage.Error, Enumerable.Empty<TblBooking>());
            }
        }

        public async ValueTask<ApiResponse<bool>> AcceptRejectBooking(int bookingId, string status)
        {
            var userIdClaim = _httpContext.HttpContext?.User?
                  .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var role = _httpContext.HttpContext?.User?
                .FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return new ApiResponse<bool>("Unauthorized", false, ResponseMessage.Error, false);

            if (role != "Artist")
                return new ApiResponse<bool>("Only artists can approve bookings", false, ResponseMessage.Error, false);

            long artistId = long.Parse(userIdClaim);
            var booking = (await uOW.GenricRepository<TblBooking>().GetAllAsync(x => x.BookingId == bookingId)).FirstOrDefault();
            if (status == "Approved")
            {
                booking.Status = "Approved";
            }
            else if (status == "Rejected")
            {
                booking.Status = "Rejected";
            }
            await this.uOW.SaveAsync();
            return new ApiResponse<bool>(status == "Approved" ? Message.BookingAccepted : Message.BookingRejected, true, ResponseMessage.Ok, true);
        

        }


        //public async ValueTask<ApiResponse<ArtistDashboardDto>> ArtistDashboard()
        //{
        //    try
        //    {
        //        long userid = Nameidentifier.GetUserId(_httpContext.HttpContext);
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("@UserId", userid);
        //        var response = (await repository.GetAllAsync(StoredProcedures.ArtistDashboard, parameters)).FirstOrDefault();
        //        var result = new ArtistDashboardDto
        //        {
        //            UserName = response.UserName,
        //            PendingBookings = response.PendingBookings,
        //            UpcomingShows = response.UpcomingShows,
        //            AverageRating = response.AverageRating,
        //            ApprovedBookingsThisMonth = response.ApprovedBookingsThisMonth,
        //            LifetimeEarnings = response.LifetimeEarnings,
        //            MonthlyEarnings = response.MonthlyEarnings,

        //            RecentRequests = string.IsNullOrEmpty(response.RecentRequests)
        //       ? new List<RecentRequest>()
        //       : JsonConvert.DeserializeObject<List<RecentRequest>>(response.RecentRequests),

        //            PendingRequests = string.IsNullOrEmpty(response.PendingRequests)
        //       ? new List<PendingRequest>()
        //       : JsonConvert.DeserializeObject<List<PendingRequest>>(response.PendingRequests)
        //        };
        //        return result != null ? new ApiResponse<ArtistDashboardDto>(Message.Retrieved, true, ResponseMessage.Ok, result) :
        //            new ApiResponse<ArtistDashboardDto>(Message.NoRecord, false, ResponseMessage.Error, new ArtistDashboardDto());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<ArtistDashboardDto>(ex.Message, false, ResponseMessage.Error, new ArtistDashboardDto());
        //    }
        //}

        public async ValueTask<ApiResponse<ArtistDashboardDto>> ArtistDashboard()
        {
            try
            {
                long userid = Nameidentifier.GetUserId(_httpContext.HttpContext);
                if (userid==0)
                {
                    return new ApiResponse<ArtistDashboardDto>(Message.Unauthorized, false, ResponseMessage.Error, new ArtistDashboardDto());
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userid);

                var response = (await repository.GetAllAsync(StoredProcedures.ArtistDashboard, parameters)).FirstOrDefault();

                if (response == null)
                {
                    return new ApiResponse<ArtistDashboardDto>(
                        Message.NoRecord, false, ResponseMessage.Error, new ArtistDashboardDto());
                }
                var result = new ArtistDashboardDto
                {
                    UserName = response.UserName,
                    PendingBookings = response.PendingBookings,
                    UpcomingShows = response.UpcomingShows,
                    AverageRating = response.AverageRating,
                    ApprovedBookingsThisMonth = response.ApprovedBookingsThisMonth,
                    LifetimeEarnings = response.LifetimeEarnings,
                    MonthlyEarnings = response.MonthlyEarnings,
                    PendingRequestlist = !string.IsNullOrEmpty(response?.PendingRequests) ? JsonHelper<PendingRequest>.DeserializeObject(response.PendingRequests).ToList() : new List<PendingRequest>(),        
                    RecentRequestlist = !string.IsNullOrEmpty(response?.RecentRequests) ?JsonHelper<RecentRequest>.DeserializeObject(response.RecentRequests).ToList():new List<RecentRequest>(),
                    TotalReviewMembers = response?.TotalReviewMembers

                };

                return new ApiResponse<ArtistDashboardDto>(
            Message.Retrieved, true, ResponseMessage.Ok, result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ArtistDashboardDto>(
                    ex.Message, false, ResponseMessage.Error, new ArtistDashboardDto());
            }
        }
    }
}
