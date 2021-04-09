using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TGPro.Data.Enums;

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
        public Sex Sex { get; set; }
        public string AccessToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string PublicId { get; set; }
    }
}
