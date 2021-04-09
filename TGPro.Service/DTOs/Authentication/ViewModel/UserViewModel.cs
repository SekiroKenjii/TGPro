using TGPro.Data.Entities;

namespace TGPro.Service.DTOs.Authentication.ViewModel
{
    public class UserViewModel
    {
        public AppUser User { get; set; }
        public string Roles { get; set; }
    }
}
