using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TGPro.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
    }
}
