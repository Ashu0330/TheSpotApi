using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
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
    public class BookingService : IBookingService
    {
        private readonly IUOW uOW;
        private readonly IHttpContextAccessor _httpContext;

        public BookingService(IUOW uOW, IHttpContextAccessor _httpContext)
        {
            this.uOW = uOW;
            this._httpContext = _httpContext;
        }
        public async ValueTask<ApiResponse<bool>> BookArtist(TblBooking model)
        {
            try
            {
                var userId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return new ApiResponse<bool>("Unauthorized", false, ResponseMessage.Error, false);
                model.BookedByUserId = int.Parse(userId);
                var existing = (await uOW.GenricRepository<TblBooking>().GetAllAsync(x => x.ArtistId == model.ArtistId && x.BookingDate == model.BookingDate && x.Status == "Accepted" && x.BookingId != model.BookingId)).FirstOrDefault();

                if (existing != null)
                {
                    return new ApiResponse<bool>(Message.BookingAlreadyExist, false, ResponseMessage.Error, false);
                }
                model.Status = "Pending";
                model.CreatedAt = DateTime.Now;
                await this.uOW.GenricRepository<TblBooking>().AddAsync(model);
                await this.uOW.SaveAsync();
                return new ApiResponse<bool>(existing == null ? Message.Saved : Message.Updated, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }

        public async ValueTask<ApiResponse<bool>> UpdateBooking(TblBooking model)
        {
            try
            {
                var booking = (await this.uOW.GenricRepository<TblBooking>().GetAllAsync(x => x.BookingId == model.BookingId)).FirstOrDefault();
                if (booking == null)

                    return new ApiResponse<bool>(Message.NotExist, false, ResponseMessage.Error, false);

                if (booking.Status != "Pending")
                    return new ApiResponse<bool>("Only pending bookings can be updated", false, ResponseMessage.Error, false);
                if (booking.Status == "Accepted")
                {
                    var conflict = await this.uOW.GenricRepository<TblBooking>().GetAllAsync(x => x.BookingId == model.BookingId && x.ArtistId == model.ArtistId && x.BookingDate == model.BookingDate && x.Status == "Accepted");
                    if (conflict.Any())
                        return new ApiResponse<bool>(Message.BookingAlreadyExist, false, ResponseMessage.Error, false);
                }
                booking.EventId = model.EventId;
                booking.LoungeId = model.LoungeId;
                booking.BookingDate = model.BookingDate;
                booking.Amount = model.Amount;
                booking.Status = model.Status;
                uOW.GenricRepository<TblBooking>().Update(booking);
                await uOW.SaveAsync();
                return new ApiResponse<bool>(Message.Updated, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }
     
        public async ValueTask<ApiResponse<bool>> CancelBooking(int bookingId)
        {
            try
            {
                var userIdClaim = _httpContext.HttpContext?
                    .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return new ApiResponse<bool>(Message.Unauthorized, false, ResponseMessage.Error, false);

                int userId = Convert.ToInt32(userIdClaim);

                var booking = (await uOW.GenricRepository<TblBooking>()
                    .GetAllAsync(x => x.BookingId == bookingId))
                    .FirstOrDefault();

                if (booking == null)
                    return new ApiResponse<bool>("Booking not found", false, ResponseMessage.NoRecords, false);

                if (booking.BookedByUserId != userId)
                    return new ApiResponse<bool>("You cannot cancel this booking", false, ResponseMessage.Error, false);

                if (booking.Status == "Completed" || booking.Status == "Rejected")
                    return new ApiResponse<bool>(Message.UnauthorizedCancelled, false, ResponseMessage.Error, false);
                booking.Status = "Cancelled";
                uOW.GenricRepository<TblBooking>().Update(booking);
                await uOW.SaveAsync();

                return new ApiResponse<bool>(Message.BookingCancelled, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }
    }
}
