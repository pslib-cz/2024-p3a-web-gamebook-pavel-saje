using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class InteractibleControllerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Interactibles");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Interactibles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Interactibles");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Interactibles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
