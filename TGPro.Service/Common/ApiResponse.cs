using System;
using System.Collections.Generic;
using System.Text;

namespace TGPro.Service.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
