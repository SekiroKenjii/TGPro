using Microsoft.AspNetCore.Http;
using TGPro.Data.Enums;

namespace TGPro.Service.DTOs.Authentication
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
