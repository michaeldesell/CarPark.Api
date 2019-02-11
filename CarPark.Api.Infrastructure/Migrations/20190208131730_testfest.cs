using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPark.Api.Infrastructure.Migrations
{
    public partial class testfest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "f163a0a7-4404-482c-aaa2-cafd681573d0", "a@b.se", true, null, null, false, null, null, null, "Banan", null, true, null, false, "CarParkWeb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "f163a0a7-4404-482c-aaa2-cafd681573d0" });
        }
    }
}
