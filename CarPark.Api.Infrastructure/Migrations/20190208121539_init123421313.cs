using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPark.Api.Infrastructure.Migrations
{
    public partial class init123421313 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "78ef74de-09d5-4c4d-a1c0-fda10073459e", "9f2ab92c-c653-4152-97c0-9e3fd0d23f7a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "78ef74de-09d5-4c4d-a1c0-fda10073459e", 0, "9f2ab92c-c653-4152-97c0-9e3fd0d23f7a", "a@b.se", false, null, null, false, null, null, null, "Banan", null, false, null, false, "CarParkWeb" });
        }
    }
}
