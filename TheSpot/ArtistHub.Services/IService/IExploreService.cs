using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using static ArtistHub.Presentation.Domain.ArtistExploreFilterModel;

namespace ArtistHub.Services.IService
{
    public interface IExploreService
    {
        ValueTask<ApiResponse<TblArtist>> GetArtistByUserId(long userId);
        ValueTask<ApiResponse<IEnumerable<TblArtist>>> GetAllArtist();
        ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetArtistByCategory(ArtistExploreFilterModel model);
        ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetLoungeByCategory(LoungeExploreFilterModel model);
        ValueTask<ApiResponse<IEnumerable<EventDto>>> GetEventByCategory(EventExploreFilterModel model);

    }
}
