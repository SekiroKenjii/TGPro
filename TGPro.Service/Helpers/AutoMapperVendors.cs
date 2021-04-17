using AutoMapper;
using TGPro.Data.Entities;
using TGPro.Service.DTOs.Vendors;

namespace TGPro.Service.Helpers
{
    public class AutoMapperVendors : Profile
    {
        public AutoMapperVendors()
        {
            CreateMap<VendorRequest, Vendor>();
        }
    }
}
