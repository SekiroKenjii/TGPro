using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class AddPublicIdtoAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "de32b5a2-3a0d-4023-bb6e-43ce2eccc9c9", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98f69526-3794-4152-b8a0-2e2382dd48c8", "AQAAAAEAACcQAAAAELqT+rcGt2w1TcoWwas4tduj/WI875XF64BcRAwzzI/te3MqVSo3urlHXUoAHjxR8A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "ebf53ec7-6101-474e-8130-5ec1a56aeaa1", "admin" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d46b51d2-4ed0-4dcd-aa3d-f4d4660effb0", "AQAAAAEAACcQAAAAEJeUQnqZwPUoe4HNU17Ii10l2qbFfuhptMLzVkieKnMxh9/jV8Lj8F8b83Mo+w76Xg==" });
        }
    }
}
