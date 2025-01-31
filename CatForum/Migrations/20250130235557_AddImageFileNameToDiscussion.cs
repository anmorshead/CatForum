using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatForum.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFileNameToDiscussion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Discussion",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Discussion");
        }
    }
}
