using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Dto
{
    public class StatusResponse<T> where T: class
    {
        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }   
        public T? Data { get; set; } 

        public static StatusResponse<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new StatusResponse<T>
            {
                Data = data,
                StatusCode = (int)statusCode,
                IsSuccess = true,
                Message = "Process Has Been Successfully Completed!",
            };
        }

        public static StatusResponse<T> Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new StatusResponse<T>
            {
                Data = default,
                StatusCode = (int)statusCode,
                IsSuccess = true,
                Message = "Process Has Been Successfully Completed!",
            };
        }

        public static StatusResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new StatusResponse<T>
            {
                StatusCode = (int)statusCode,
                IsSuccess = false,
                Message = message,
            };
        }
    }
}
