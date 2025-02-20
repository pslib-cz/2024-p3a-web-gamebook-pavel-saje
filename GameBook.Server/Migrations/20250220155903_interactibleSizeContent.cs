using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class interactibleSizeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "Interactibles");

            migrationBuilder.AddColumn<int>(
                name: "size",
                table: "LocationContents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "LocationContents");

            migrationBuilder.AddColumn<int>(
                name: "size",
                table: "Interactibles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
