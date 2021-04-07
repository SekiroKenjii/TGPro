using System;
using System.Collections.Generic;
using System.Text;

namespace TGPro.Service.Common
{
    public class ApiErrorResponse<T> : ApiResponse<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResponse()
        {
        }

        public ApiErrorResponse(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResponse(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}
