using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IUserService
    {
        #region UserRegion
        ValueTask<ApiResponse<IEnumerable<UserDto>>> GetUsers();
        ValueTask<ApiResponse<UserDto>> GetUsersById(long id);
        #endregion

        #region ArtistRegion


        #endregion

        #region LoungeRegion


        //ValueTask<ApiResponse<TblEvent>> GetAllEvents(filterModel model);

        #endregion



    }
}
