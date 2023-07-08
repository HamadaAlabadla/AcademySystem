using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademySystem.EF.Migrations
{
    public partial class EditServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "2ae1d1e1-00d2-4bbe-846e-b88f2e9a6fd1", "a1f0683d-40ec-4e92-93fe-22245b844950", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5fb2d251-875e-467f-8a01-ec43edd8b4d2", 0, "452f907d-db61-4236-af38-b1085358c3b5", "Admin@admin", false, true, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0595195186", false, "89032b4c-717f-4f4d-b94e-9841de41e5f7", false, "Admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2ae1d1e1-00d2-4bbe-846e-b88f2e9a6fd1", "5fb2d251-875e-467f-8a01-ec43edd8b4d2" });

            migrationBuilder.InsertData(
                table: "Logings",
                columns: new[] { "appUserId", "IsLogging" },
                values: new object[] { "5fb2d251-875e-467f-8a01-ec43edd8b4d2", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ae1d1e1-00d2-4bbe-846e-b88f2e9a6fd1", "5fb2d251-875e-467f-8a01-ec43edd8b4d2" });

            migrationBuilder.DeleteData(
                table: "Logings",
                keyColumn: "appUserId",
                keyValue: "5fb2d251-875e-467f-8a01-ec43edd8b4d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ae1d1e1-00d2-4bbe-846e-b88f2e9a6fd1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5fb2d251-875e-467f-8a01-ec43edd8b4d2");

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
    }
}
