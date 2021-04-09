using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;

namespace TGPro.Service.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //Identity
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<TGProDbContext>()
                .AddDefaultTokenProviders();

            //JWT Bearer
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Tokens:Issuer"],
                        // ValidAudience = config["Tokens:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
                    };
                });

            //Cloudinary
            services.Configure<CloudinarySettings>(config.GetSection(ConstantStrings.CloudinarySetting));

            return services;
        }
    }
}
