using Microsoft.EntityFrameworkCore.Migrations;

namespace PansyDev.Shetter.Infrastructure.Migrations
{
    public partial class AddImagesToPostVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "PostVersions",
                type: "jsonb",
                nullable: false,
                defaultValue: "[]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "PostVersions");
        }
    }
}
