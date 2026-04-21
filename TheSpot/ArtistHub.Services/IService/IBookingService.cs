using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IBookingService
    {
        ValueTask<ApiResponse<bool>> BookArtist(TblBooking model);
        ValueTask<ApiResponse<bool>> UpdateBooking(TblBooking model);
        ValueTask<ApiResponse<bool>> CancelBooking(int bookingid);
    }
}
