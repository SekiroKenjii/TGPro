using AutoMapper;
using TGPro.Data.Entities;
using TGPro.Service.DTOs.Authentication;

namespace TGPro.Service.Helpers
{
    public class AutoMapperUsers : Profile
    {
        public AutoMapperUsers()
        {
            CreateMap<UserRequest, AppUser>();
        }
    }
}
