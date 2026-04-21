using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface ILoungeService
    {
        ValueTask<ApiResponse<bool>> CreateLongue(TblLounge model);
        ValueTask<ApiResponse<bool>> CreateEvent(TblEvent model);
        ValueTask<ApiResponse<IEnumerable<TblEvent>>> GetAllEvent(filterModel model);
    }
}
