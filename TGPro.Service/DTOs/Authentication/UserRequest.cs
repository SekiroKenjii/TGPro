using Microsoft.AspNetCore.Http;
using TGPro.Data.Enums;

namespace TGPro.Service.DTOs.Authentication
{
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SubRole { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public Gender Gender { get; set; }
    }
}
