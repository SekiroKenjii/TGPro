using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class ReplaceSexToGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "AppUsers",
                newName: "Gender");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d3c13fc4-d9f3-4a19-a6ba-dc5912588893");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9cfc3d28-e8ae-484c-86ee-fcf1fafa6299", "AQAAAAEAACcQAAAAEHUrestEIAaOpjIp7aVIIINbf2pGX1LJN6T2swdi3MRr/s7wHp+SHdP//O3mgYYZHA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AppUsers",
                newName: "Sex");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b935e103-87d1-4458-881b-2eb63089463d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "92bdb775-155d-4720-a2b0-57d53ad5b223", "AQAAAAEAACcQAAAAEF8Ga9Wb0o+0Q+Kmo0XMj9n3vcXJeJ7BgZi4bsP6PkjzSdOMy3coqRVcoJW3jzX0Nw==" });
        }
    }
}
