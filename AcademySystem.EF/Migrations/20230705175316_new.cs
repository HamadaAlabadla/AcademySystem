using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademySystem.EF.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01d1b970-b548-4e71-bc37-b5ca901cd203", "a2285977-b7de-4b22-bdd9-1555886ae56f" });

            migrationBuilder.DeleteData(
                table: "Logings",
                keyColumn: "appUserId",
                keyValue: "a2285977-b7de-4b22-bdd9-1555886ae56f");

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
                values: new object[] { "f37e946c-0af2-4ec4-be20-037b7bb7c08b", "b0b73329-a8ec-4032-8396-241c3c827032", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf", 0, "bc658eb0-90cd-46b5-bb4b-6b3962ef5cc3", "Admin@admin", false, true, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0595195186", false, "742b1c35-6431-47f8-8894-fcff62c0d73b", false, "Admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f37e946c-0af2-4ec4-be20-037b7bb7c08b", "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf" });

            migrationBuilder.InsertData(
                table: "Logings",
                columns: new[] { "appUserId", "IsLogging" },
                values: new object[] { "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f37e946c-0af2-4ec4-be20-037b7bb7c08b", "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf" });

            migrationBuilder.DeleteData(
                table: "Logings",
                keyColumn: "appUserId",
                keyValue: "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f37e946c-0af2-4ec4-be20-037b7bb7c08b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dc4349b-ca88-46c3-bdd7-4ecb093f82cf");

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
    }
}
