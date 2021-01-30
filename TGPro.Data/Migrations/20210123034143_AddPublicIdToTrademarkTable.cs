using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class AddPublicIdToTrademarkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Trademarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Trademarks");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f33533ca-1bc8-4317-83f6-0497eca916c1");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "435f08c7-7b87-4d4c-b49b-137766152119", "AQAAAAEAACcQAAAAEMAK3vyKN0aIEhKWnMDhG/qbsIeLVsJeZm1yOZ60RiDFpYl9F76sgtJgcKqmuI2+Ew==" });
        }
    }
}
