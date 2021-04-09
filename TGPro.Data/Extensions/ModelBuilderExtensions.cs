using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using TGPro.Data.Entities;

namespace TGPro.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Seed Admin User
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Application administrator role"
            });
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "Admin",
                NormalizedUserName = "admin",
                Email = "trungthuongvo109@gmail.com",
                NormalizedEmail = "trungthuongvo109@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Thuong0165@"),
                SecurityStamp = string.Empty,
                PhoneNumber = "0375274267",
                PhoneNumberConfirmed = true,
                FirstName = "Võ Trung",
                LastName = "Thường",
                Gender = Enums.Gender.Male,
                Address = "KTX Khu B, Đại Học Quốc Gia TPHCM",
                City = "Thành phố Hồ Chí Minh",
                Country = "Việt Nam"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
