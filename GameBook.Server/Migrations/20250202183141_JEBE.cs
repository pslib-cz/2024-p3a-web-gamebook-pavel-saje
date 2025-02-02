using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class JEBE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromInteractibleID",
                table: "Dialogs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_FromInteractibleID",
                table: "Dialogs",
                column: "FromInteractibleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Interactibles_FromInteractibleID",
                table: "Dialogs",
                column: "FromInteractibleID",
                principalTable: "Interactibles",
                principalColumn: "InteractibleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Interactibles_FromInteractibleID",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_FromInteractibleID",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "FromInteractibleID",
                table: "Dialogs");
        }
    }
}
