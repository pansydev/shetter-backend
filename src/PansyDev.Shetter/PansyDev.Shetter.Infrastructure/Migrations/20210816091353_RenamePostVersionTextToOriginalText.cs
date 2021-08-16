using Microsoft.EntityFrameworkCore.Migrations;

namespace PansyDev.Shetter.Infrastructure.Migrations
{
    public partial class RenamePostVersionTextToOriginalText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "PostVersions",
                newName: "OriginalText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalText",
                table: "PostVersions",
                newName: "Text");
        }
    }
}
