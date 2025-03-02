using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatForum.Migrations
{
    /// <inheritdoc />
    public partial class AddUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Discussion",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.DropIndex(
                name: "IX_Discussion_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Discussion");
        }
    }
}
