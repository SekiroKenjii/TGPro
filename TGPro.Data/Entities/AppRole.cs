using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TGPro.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
