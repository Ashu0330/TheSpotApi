using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IAdminService
    {
        ValueTask<ApiResponse<bool>> ApproveArtist(long id);
        ValueTask<ApiResponse<bool>> RejectArtist(long id);
        ValueTask<ApiResponse<bool>> ApproveLounge(long id);
        ValueTask<ApiResponse<bool>> RejectLounge(long id);
        ValueTask<ApiResponse<bool>> ApproveEvent(int id);
        ValueTask<ApiResponse<bool>> RejectEvent(int id);
        ValueTask<ApiResponse<bool>> DeleteArtist(long id);
        ValueTask<ApiResponse<bool>> InActiveArtist(long id);
        ValueTask<ApiResponse<bool>> DeleteLounge(int id);
        ValueTask<ApiResponse<bool>> InActiveLounge(int id);
        ValueTask<ApiResponse<bool>> DeleteEvent(int id);
        ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetAllArtistApproval();
        ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetAllLoungeApproval();
    }
}
