using Microsoft.EntityFrameworkCore.Migrations;

namespace PansyDev.Shetter.Infrastructure.Migrations
{
    public partial class AddAuthorForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostAuthors_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "PostAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostAuthors_AuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts");
        }
    }
}
