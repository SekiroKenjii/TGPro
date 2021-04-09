using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGPro.Data.Migrations
{
    public partial class ReplaceRefreshTokenToAccessTokenInAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "AppUsers",
                newName: "AccessToken");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "AppUsers",
                newName: "RefreshToken");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "de32b5a2-3a0d-4023-bb6e-43ce2eccc9c9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98f69526-3794-4152-b8a0-2e2382dd48c8", "AQAAAAEAACcQAAAAELqT+rcGt2w1TcoWwas4tduj/WI875XF64BcRAwzzI/te3MqVSo3urlHXUoAHjxR8A==" });
        }
    }
}
