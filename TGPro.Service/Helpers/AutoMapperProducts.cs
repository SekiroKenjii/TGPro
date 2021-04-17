using AutoMapper;
using System.Collections.Generic;
using TGPro.Data.Entities;
using TGPro.Service.DTOs.Products;
using TGPro.Service.DTOs.Products.ViewModel;

namespace TGPro.Service.Helpers
{
    public class AutoMapperProducts : Profile
    {
        public AutoMapperProducts()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Condition, ConditionViewModel>();
            CreateMap<Vendor, VendorViewModel>();
            CreateMap<Trademark, TrademarkViewModel>();
            CreateMap<Demand, DemandViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
        }
    }
}
