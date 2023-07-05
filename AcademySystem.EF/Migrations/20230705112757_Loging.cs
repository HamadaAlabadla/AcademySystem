using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademySystem.EF.Migrations
{
    public partial class Loging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Logings",
                columns: table => new
                {
                    appUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsLogging = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logings", x => x.appUserId);
                    table.ForeignKey(
                        name: "FK_Logings_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01d1b970-b548-4e71-bc37-b5ca901cd203", "a1251c91-1785-4bc9-9de5-7b0f262ebf87", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a2285977-b7de-4b22-bdd9-1555886ae56f", 0, "80047774-0cf5-4c98-a55b-583e954a4968", "Admin@admin", false, false, null, null, null, "+OcDqTJiQbqltZ9kEoZDeKV/NkgShbqXXowVUCUR/zBONBRxA==", "0595195186", false, "d40415d9-3584-4e97-b9b6-d9cbbea345da", false, "user" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01d1b970-b548-4e71-bc37-b5ca901cd203", "a2285977-b7de-4b22-bdd9-1555886ae56f" });

            migrationBuilder.InsertData(
                table: "Logings",
                columns: new[] { "appUserId", "IsLogging" },
                values: new object[] { "a2285977-b7de-4b22-bdd9-1555886ae56f", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logings");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01d1b970-b548-4e71-bc37-b5ca901cd203", "a2285977-b7de-4b22-bdd9-1555886ae56f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d1b970-b548-4e71-bc37-b5ca901cd203");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2285977-b7de-4b22-bdd9-1555886ae56f");

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
    }
}
