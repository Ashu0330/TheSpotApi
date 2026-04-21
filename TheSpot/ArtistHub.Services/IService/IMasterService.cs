using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.IService
{
    public interface IMasterService
    {
        ValueTask<ApiResponse<bool>> CreateCategory(CategoryMaster model);
        ValueTask<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategory();
        ValueTask<ApiResponse<bool>> DeleteCategory(int id);
        ValueTask<ApiResponse<bool>> CreateRoles(TblRoleMaster model);
        ValueTask<ApiResponse<IEnumerable<TblRoleMaster>>> GetAllRoles();

    }
}
