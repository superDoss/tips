using Microsoft.EntityFrameworkCore.Migrations;

namespace Tips.Migrations.TipsIdentityDb
{
    public partial class AdminroleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7517e4d-8abf-4868-8ff1-8d18a5dc4bb9", "53c5117a-bfb0-4e26-93e8-016f66f1dcf4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c7517e4d-8abf-4868-8ff1-8d18a5dc4bb9", "53c5117a-bfb0-4e26-93e8-016f66f1dcf4" });
        }
    }
}
