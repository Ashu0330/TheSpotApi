using ArtistHub.DAL.Repository;
using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Repository.RepositoryServices;
using ArtistHub.Services.IService;
using Dapper;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static ArtistHub.Presentation.Domain.ArtistExploreFilterModel;

namespace ArtistHub.Services.Service
{
    public class ExploreService : IExploreService
    {
        private readonly IUOW uOW;
        private readonly IRepositoryService<ArtistDto> AtrRepository;
        private readonly IRepositoryService<LoungeDto> LonRepository;
        private readonly IRepositoryService<EventDto> eveRepository;
        private readonly IHttpContextAccessor _httpContext;
        public ExploreService(IUOW uOW, IRepositoryService<ArtistDto> artRepository, IRepositoryService<LoungeDto> LonRepository, 
            IRepositoryService<EventDto> eveRepository, IHttpContextAccessor _httpcontext)
        {
            this.uOW = uOW;
            this.AtrRepository = artRepository;
            this.LonRepository = LonRepository;
            this.eveRepository = eveRepository;
            this._httpContext = _httpcontext;

        }

        public async ValueTask<ApiResponse<TblArtist>> GetArtistByUserId(long userId)
        {
            try
            {
                if (userId==0)
                {
                    userId = Nameidentifier.GetUserId(_httpContext.HttpContext);
                }
                var result = await this.uOW.GenricRepository<TblArtist>().GetAllAsync(a => a.UserId == userId && a.IsDeleted == false);
                return result.Any() ? new ApiResponse<TblArtist>(Message.Retrieved, true, ResponseMessage.Ok, result.First()) :
                                    new ApiResponse<TblArtist>(Message.NoRecord, true, ResponseMessage.NoRecords, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TblArtist>(ex.Message, false, ResponseMessage.Error, null);
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<TblArtist>>> GetAllArtist()
        {
            try
            {
                var result = await this.uOW.GenricRepository<TblArtist>().GetAllAsync(x => x.IsDeleted == false);
                return result.Any() ? new ApiResponse<IEnumerable<TblArtist>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                    new ApiResponse<IEnumerable<TblArtist>>(Message.NoRecord, true, ResponseMessage.NoRecords, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<TblArtist>>(ex.Message, false, ResponseMessage.Error, null);
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<ArtistDto>>> GetArtistByCategory(ArtistExploreFilterModel model)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", OperationType.GetArtistbyCategory);
                parameters.Add("@CategoryId", model.CategoryId);
                parameters.Add("@Status", model.Status);
                parameters.Add("@PricePerShow", model.PricePerShow);
                parameters.Add("@City", model.City);
                parameters.Add("@Rating", model.Rating);
                parameters.Add("@TotalShows", model.TotalShows);
                parameters.Add("@BookingDate", model.BookingDate);
                parameters.Add("@SortBy", model.SortBy);

                
                var result = await this.AtrRepository.GetAllAsync(StoredProcedures.ExploreProcedure, parameters);
                result.Select(x =>
                {
                    x.FileUrl = $"{ImageEnvironment.Baseurl}/{x.FileUrl}";
                    return x;
                }).ToList();
                return result.Any() ? new ApiResponse<IEnumerable<ArtistDto>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                    new ApiResponse<IEnumerable<ArtistDto>>(Message.NoRecord, true, ResponseMessage.NoRecords, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ArtistDto>>(ex.Message, false, ResponseMessage.Error, null);
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<LoungeDto>>> GetLoungeByCategory(LoungeExploreFilterModel model)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", OperationType.GetloungesbyCategory);
                parameters.Add("@LoungeName", model.LoungeName);
                parameters.Add("@Capacity", model.Capacity);
                parameters.Add("@City", model.City);
                parameters.Add("@Rating", model.Rating);
                parameters.Add("@SortBy", model.SortBy);
                var result = await this.LonRepository.GetAllAsync(StoredProcedures.ExploreProcedure, parameters);
                return result.Any() ? new ApiResponse<IEnumerable<LoungeDto>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                    new ApiResponse<IEnumerable<LoungeDto>>(Message.NoRecord, true, ResponseMessage.NoRecords, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<LoungeDto>>(ex.Message, false, ResponseMessage.Error, null);
            }
        }
        public async ValueTask<ApiResponse<IEnumerable<EventDto>>> GetEventByCategory(EventExploreFilterModel model)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", OperationType.GetEventbyCategory);
                parameters.Add("@ArtistId", model.ArtistId);
                parameters.Add("@LoungeId", model.LoungeId);
                parameters.Add("@EventFromDate", model.EventFromDate);
                parameters.Add("@EventToDate", model.EventToDate);
                parameters.Add("@MinTicketPrice", model.MinTicketPrice);
                parameters.Add("@MaxTicketPrice", model.MaxTicketPrice);
                parameters.Add("@EventStatus", model.EventStatus);
                parameters.Add("@City", model.City);
                parameters.Add("@CategoryId", model.CategoryId);
                parameters.Add("@SortBy", model.SortBy);
                var result = await this.eveRepository.GetAllAsync(StoredProcedures.ExploreProcedure, parameters);
                return result.Any() ? new ApiResponse<IEnumerable<EventDto>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                    new ApiResponse<IEnumerable<EventDto>>(Message.NoRecord, true, ResponseMessage.NoRecords, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EventDto>>(ex.Message, false, ResponseMessage.Error, null);
            }
        }
    }
}