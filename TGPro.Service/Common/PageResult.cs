using System;
using System.Collections.Generic;
using System.Text;

namespace TGPro.Service.Common
{
    public class PageResult<T>: PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}
