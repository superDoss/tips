using Microsoft.EntityFrameworkCore.Migrations;

namespace Tips.Migrations
{
    public partial class loaction_to_tips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Tips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Tips");
        }
    }
}
