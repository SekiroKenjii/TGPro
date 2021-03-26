using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Catalog.Categories;
using TGPro.Service.Catalog.Conditions;
using TGPro.Service.Catalog.Demands;
using TGPro.Service.Catalog.Trademarks;
using TGPro.Service.Catalog.Vendors;
using TGPro.Service.Common;
using TGPro.Service.SystemResources;

namespace TGPro.BackendAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //add CORS
            services.AddCors(c =>
            {
                c.AddPolicy(ConstantStrings.AllowOrigin, options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<TGProDbContext>()
                .AddDefaultTokenProviders();

            //DB connection
            services.AddDbContext<TGProDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConstantStrings.DbConnectionString)));

            //declare DI
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IConditionService, ConditionService>();
            services.AddTransient<IDemandService, DemandService>();
            services.AddTransient<ITrademarkService, TrademarkService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.Configure<CloudinarySettings>(Configuration.GetSection(ConstantStrings.CloudinarySetting));

            services.AddControllers();

            //add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ConstantStrings.OpenApiVersion, new OpenApiInfo { Title = ConstantStrings.OpenApiTitle, Version = ConstantStrings.OpenApiVersion });
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(ConstantStrings.SwaggerUrl, ConstantStrings.SwaggerName));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
