using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class AddSexToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "f33533ca-1bc8-4317-83f6-0497eca916c1", "Application administrator role", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "Sex", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "KTX Khu B, Đại Học Quốc Gia TPHCM", "Thành phố Hồ Chí Minh", "435f08c7-7b87-4d4c-b49b-137766152119", "Việt Nam", "trungthuongvo109@gmail.com", true, "Võ Trung", "Thường", 0, false, null, "trungthuongvo109@gmail.com", "admin", "AQAAAAEAACcQAAAAEMAK3vyKN0aIEhKWnMDhG/qbsIeLVsJeZm1yOZ60RiDFpYl9F76sgtJgcKqmuI2+Ew==", "0375274267", true, null, "", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"));

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "AppUsers");
        }
    }
}
