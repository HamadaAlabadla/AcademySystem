using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademySystem.EF.Migrations
{
    public partial class Create_CB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ggg",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_appUserId",
                table: "Students",
                column: "appUserId",
                unique: true,
                filter: "[appUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_appUserId",
                table: "Students",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_appUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_appUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "ggg",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
