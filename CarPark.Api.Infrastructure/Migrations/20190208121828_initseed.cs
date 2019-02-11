using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPark.Api.Infrastructure.Migrations
{
    public partial class initseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2173f1f0-9b99-4f37-826c-011e5846ddf0", "199003ce-0471-4c68-a757-086c1dd098b5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2173f1f0-9b99-4f37-826c-011e5846ddf0", 0, "199003ce-0471-4c68-a757-086c1dd098b5", "a@b.se", false, "CarParkWeb", null, false, null, null, null, "Banan", null, false, null, false, null });
        }

      
    }
}
