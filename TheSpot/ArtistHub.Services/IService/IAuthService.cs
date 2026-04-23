using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IAuthService
    {
        ValueTask<ApiResponse<UserDto>> LoginUser(LoginRequestDto model);
        ValueTask<ApiResponse<long>> RegisterUser(UserDto model);


    }
}
