using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Model
{
    public class BaseResponse
    {
        public string Message { get; set; } = "Sucess";

        public int MessageCode { get; set; } = StatusCodes.Status200OK;

        public object Data { get; set; }
    }
}
