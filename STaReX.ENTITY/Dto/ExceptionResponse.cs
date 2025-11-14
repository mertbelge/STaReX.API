using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Dto
{
    public class ExceptionResponse: Exception
    {
        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ExceptionResponse(HttpStatusCode statusCode, string message) 
        {
            StatusCode = (int)statusCode;
            IsSuccess = false;
            Message = message;
        }



    }
}
