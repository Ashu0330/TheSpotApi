using ArtistHub.DAL.UOW;
using ArtistHub.Domain.Models;
using ArtistHub.Presentation.Domain;
using ArtistHub.Presentation.Helper;
using ArtistHub.Services.IService;
using KFPLSkillUp.Presentation.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Services.Service
{
    public class LoungeService : ILoungeService
    {
        private readonly IUOW uOW;
        private readonly IHttpContextAccessor _httpContext;

        public LoungeService(IUOW uOW, IHttpContextAccessor _httpContext)
        {
            this.uOW = uOW;
            this._httpContext = _httpContext;
        }

        public async ValueTask<ApiResponse<bool>> CreateLongue(TblLounge model)
        {
            try
            {
                var userid = Nameidentifier.GetUserId(_httpContext.HttpContext);

                if (model.LoungeId != 0)
                {
                    var response = (await this.uOW.GenricRepository<TblLounge>().GetAllAsync(x => x.LoungeId == model.LoungeId)).FirstOrDefault();
                    if (response != null)
                    {
                        response.LoungeName = model.LoungeName;
                        response.Address = model.Address;
                        response.City = model.City;
                        response.Capacity = model.Capacity;
                        response.Description = model.Description;
                        this.uOW.GenricRepository<TblLounge>().Update(response);
                    }
                }
                else
                {
                    await this.uOW.GenricRepository<TblLounge>().AddAsync(model);
                }
                await this.uOW.SaveAsync();
                return new ApiResponse<bool>(Message.Success, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }

        public async ValueTask<ApiResponse<bool>> CreateEvent(TblEvent model)
        {
            try
            {
                if (model.EventId != 0)
                {
                    var response = (await this.uOW.GenricRepository<TblEvent>().GetAllAsync(filter: x => x.EventId == model.EventId && x.LoungeId == model.LoungeId)).FirstOrDefault();
                    if (response != null)
                    {
                        response.Title = model.Title;
                        response.EventDate = model.EventDate;
                        response.StartTime = model.StartTime;
                        response.EndTime = model.EndTime;
                        response.TicketPrice = model.TicketPrice;
                        response.TotalSeats = model.TotalSeats;
                        response.AvailableSeats = model.AvailableSeats;
                        response.Status = model.Status;
                        this.uOW.GenricRepository<TblEvent>().Update(response);
                    }
                }
                else
                {
                    await this.uOW.GenricRepository<TblEvent>().AddAsync(model);
                }
                await this.uOW.SaveAsync();
                return new ApiResponse<bool>(model.EventId == 0 ? Message.Saved : Message.Updated, true, ResponseMessage.Ok, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(ex.Message, false, ResponseMessage.Error, false);
            }
        }

        public async ValueTask<ApiResponse<IEnumerable<TblLounge>>> GetAllLounges()
        {
            try
            {
                var result = await this.uOW.GenricRepository<TblLounge>().GetAllAsync();
                return result.Any() ? new ApiResponse<IEnumerable<TblLounge>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                    new ApiResponse<IEnumerable<TblLounge>>(Message.NoRecord, true, ResponseMessage.NoRecords, Enumerable.Empty<TblLounge>());
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<TblLounge>>(ex.Message, false, ResponseMessage.Error, Enumerable.Empty<TblLounge>());
            }
        }

        public async ValueTask<ApiResponse<IEnumerable<TblEvent>>> GetAllEvent(filterModel model)
        {
            var result = await this.uOW.GenricRepository<TblEvent>().GetAllAsync(filter: x => (model.EventId == 0) || x.EventId == model.EventId);
            return result.Any() ? new ApiResponse<IEnumerable<TblEvent>>(Message.Retrieved, true, ResponseMessage.Ok, result) :
                                new ApiResponse<IEnumerable<TblEvent>>(Message.NoRecord, true, ResponseMessage.NoRecords, Enumerable.Empty<TblEvent>());
        }
    }
}
