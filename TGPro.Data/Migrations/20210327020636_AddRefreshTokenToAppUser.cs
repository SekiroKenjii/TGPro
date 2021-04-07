using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class AddRefreshTokenToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ebf53ec7-6101-474e-8130-5ec1a56aeaa1");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d46b51d2-4ed0-4dcd-aa3d-f4d4660effb0", "AQAAAAEAACcQAAAAEJeUQnqZwPUoe4HNU17Ii10l2qbFfuhptMLzVkieKnMxh9/jV8Lj8F8b83Mo+w76Xg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a370c81e-3d85-4637-890b-85cd252dcc99");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ccf54fa7-04ec-47bb-9cb3-9622dd73cc1c", "AQAAAAEAACcQAAAAENEO7bBj+lup9tsAQUdzsavRdLSFBWb7+hkZlgGIkJJCxOXo3XDGVl5KgQjCEW8JZQ==" });
        }
    }
}
