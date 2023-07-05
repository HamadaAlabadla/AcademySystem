using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademySystem.EF.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cfcfd3d-9117-4761-99c7-09e111d31202", "191b17b7-f079-4fee-a2e9-b08c106092e1", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d4db898e-a8cf-4a78-a4ab-4ea3b1eb0798", 0, "f4e7d767-7480-4be8-bd29-bdde794648dc", "Admin@admin", false, false, null, null, null, "AQAAAAEAACcQAAAAELIS3qn2HJsonP6MPpkal5fIT7Tri2z0LJwhhwy9n55PmaTcX0viPj9SmqYnStN6hw==", "0595195186", false, "8416372b-1494-4700-a830-9de30a7825e2", false, "user" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8cfcfd3d-9117-4761-99c7-09e111d31202", "d4db898e-a8cf-4a78-a4ab-4ea3b1eb0798" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8cfcfd3d-9117-4761-99c7-09e111d31202", "d4db898e-a8cf-4a78-a4ab-4ea3b1eb0798" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cfcfd3d-9117-4761-99c7-09e111d31202");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4db898e-a8cf-4a78-a4ab-4ea3b1eb0798");
        }
    }
}
