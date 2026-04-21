using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IArtistService
    {
        ValueTask<ApiResponse<bool>> CreateArtist(TblArtist model);
        ValueTask<ApiResponse<bool>> ArtistMedia(ArtistMediaDto model);

        ValueTask<ApiResponse<IEnumerable<TblArtistMedium>>> GetArtistMedia(int artistId);
        ValueTask<ApiResponse<IEnumerable<TblBooking>>> GetAllBooking(filterModel model);
        ValueTask<ApiResponse<bool>> AcceptRejectBooking(int bookingId, string status);
        ValueTask<ApiResponse<ArtistDashboardDto>> ArtistDashboard();

    }
}
