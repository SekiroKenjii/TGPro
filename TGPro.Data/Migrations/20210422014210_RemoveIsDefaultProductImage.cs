using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class RemoveIsDefaultProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "46f91136-52fb-42ca-af81-d9ba9b819d4d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c80dc05b-dd3e-4978-a978-6cf609bb8afd", "AQAAAAEAACcQAAAAEMnNdEoPNAanPNfJ/IWWyHoU7qx6bwCEZkhOVGA5vkpT+igyJ3poB0stPoeiihiyzQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c02ef166-de16-40c9-9ae6-d275f422e547");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bf836832-d369-4152-93e6-5d13a93f4465", "AQAAAAEAACcQAAAAEJOvWaR/TtlpGv8AAknyFQmOg47lUGmVmjrKmsSS7lNIYMVJggg1bwG2ND5plGI/7g==" });
        }
    }
}
