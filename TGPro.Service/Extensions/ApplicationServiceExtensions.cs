using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Catalog.Authentication;
using TGPro.Service.Catalog.Categories;
using TGPro.Service.Catalog.Conditions;
using TGPro.Service.Catalog.Demands;
using TGPro.Service.Catalog.ProductImages;
using TGPro.Service.Catalog.Products;
using TGPro.Service.Catalog.Trademarks;
using TGPro.Service.Catalog.Vendors;
using TGPro.Service.Common;
using TGPro.Service.Helpers;

namespace TGPro.Service.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TGProDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString(ConstantStrings.DbConnectionString)));

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IConditionService, ConditionService>();
            services.AddTransient<IDemandService, DemandService>();
            services.AddTransient<ITrademarkService, TrademarkService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();

            services.AddAutoMapper(typeof(AutoMapperProducts).Assembly);
            services.AddAutoMapper(typeof(AutoMapperVendors).Assembly);

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            return services;
        }
    }
}
