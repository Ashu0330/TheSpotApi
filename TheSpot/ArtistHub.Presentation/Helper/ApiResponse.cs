using KFPLSkillUp.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Helper
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public ApiResponse(string Message, bool IsSucces, ResponseMessage StatusCode, T? data)
        {
            this.Message = Message;
            this.IsSuccess = IsSucces;
            this.StatusCode = (int)StatusCode;
            this.Data = data;

        }
    }
}
