using ArtistHub.DAL.Repository;
using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Repository.RepositoryServices;
using ArtistHub.Services.IService;
using Dapper;
using KFPLSkillUp.Presentation.Helper;
namespace ArtistHub.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IUOW uOW;
        private readonly IRepositoryService<LoungeDto> lngservice;
        private readonly IRepositoryService<ArtistDto> artservice;
        public AdminService(IUOW uOW, IRepositoryService<LoungeDto> lngservice, IRepositoryService<ArtistDto> artservice)
        {
            this.uOW = uOW;
            this.artservice = artservice;
            this.lngservice = lngservice;

        }
        public async ValueTask<ApiResponse<bool>> ApproveArtist(long id)
        {
            var response = await this.uOW.GenricRepository<TblArtist>().GetByIdAsync(id);
            response.IsVerified = true;
            await uOW.SaveAsync();
            return new ApiResponse<bool>(Message.ArtistApproved, true, ResponseMessage.Ok, true);
        }

        public async ValueTask<ApiResponse<bool>> ApproveEvent(int id)
        {
            var response = this.uOW.GenricRepository<TblEvent>().GetByIdAsync(id).Result;
            response.Status = "Approved";
            await this.uOW.SaveAsync();
            return new ApiResponse<bool>(Message.EventApproved, true, ResponseMessage.Ok, true);
        }

        public async ValueTask<ApiResponse<bool>> ApproveLounge(long id)
        {
            var response = this.uOW.GenricRepository<TblLounge>().GetByIdAsync(id).Result;
            response.IsVerified = true;
            await this.uOW.SaveAsync();
            return new ApiResponse<bool>(Message.LoungeApproved, true, ResponseMessage.Ok, true);

        }

        public ValueTask<ApiResponse<bool>> DeleteArtist(long id)
        {
            var response = this.uOW.GenricRepository<TblArtist>().GetByIdAsync(id).Result;
            response.IsDeleted = true;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.ArtistDeleted, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> DeleteEvent(int id)
        {
            var response = this.uOW.GenricRepository<TblEvent>().GetByIdAsync(id).Result;
            response.IsDeleted = true;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.EventDeleted, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> DeleteLounge(int id)
        {
            var response = this.uOW.GenricRepository<TblLounge>().GetByIdAsync(id).Result;
            response.IsDeleted = true;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.EventDeleted, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> InActiveArtist(long id)
        {
            var response = this.uOW.GenricRepository<TblArtist>().GetByIdAsync(id).Result;
            response.IsActive = false;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.ArtistInactive, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> InActiveLounge(int id)
        {
            var response = this.uOW.GenricRepository<TblLounge>().GetByIdAsync(id).Result;
            response.IsActive = true;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.LoungeInactive, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> RejectArtist(long id)
        {
            var response = this.uOW.GenricRepository<TblArtist>().GetByIdAsync(id).Result;
            response.IsVerified = false;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.ArtistRejected, true, ResponseMessage.Ok, true));
        }

        public ValueTask<ApiResponse<bool>> RejectEvent(int id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ApiResponse<bool>> RejectLounge(long id)
        {
            var response = this.uOW.GenricRepository<TblLounge>().GetByIdAsync(id).Result;
            response.IsVerified = false;
            this.uOW.SaveAsync();
            return new ValueTask<ApiResponse<bool>>(new ApiResponse<bool>(Message.LoungeRejected, true, ResponseMessage.Ok, true));
        }
        public async ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetAllArtistApproval()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", "Artist");
                var response = await this.artservice.GetAllAsync(StoredProcedures.AdminProcedure, parameters);
                return response.Any() ? new ApiResponse<IEnumerable<ArtistDto>>
                    (Message.Retrieved, true, ResponseMessage.Ok, response) :
                    new ApiResponse<IEnumerable<ArtistDto>>(Message.NoRecord, false, ResponseMessage.Ok, null);
            }
            catch (Exception Message)
            {
                return new ApiResponse<IEnumerable<ArtistDto>>(Message.Message, false, ResponseMessage.Error, null);
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetAllLoungeApproval()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", "Lounge");
                var response = await this.lngservice.GetAllAsync(StoredProcedures.AdminProcedure, parameters);
                return response.Any() ? new ApiResponse<IEnumerable<LoungeDto>>
                    (Message.Retrieved, true, ResponseMessage.Ok, response) :
                    new ApiResponse<IEnumerable<LoungeDto>>(Message.NoRecord, false, ResponseMessage.Ok, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<LoungeDto>>(ex.Message, false, ResponseMessage.Error, null);
            }
        }

    }
}
