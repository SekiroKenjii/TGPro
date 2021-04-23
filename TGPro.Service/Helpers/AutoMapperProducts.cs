using AutoMapper;
using TGPro.Data.Entities;
using TGPro.Service.DTOs.Products;

namespace TGPro.Service.Helpers
{
    public class AutoMapperProducts : Profile
    {
        public AutoMapperProducts()
        {
            CreateMap<ProductRequest, Product>();
        }
    }
}
