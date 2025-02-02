using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class MamMasivniSykorku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ends",
                columns: table => new
                {
                    EndID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ends", x => x.EndID);
                });

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumableItems");

            migrationBuilder.DropTable(
                name: "DialogResponses");

            migrationBuilder.DropTable(
                name: "Ends");

            migrationBuilder.DropTable(
                name: "InteractiblesItems");

            migrationBuilder.DropTable(
                name: "InteractiblesOptions");

            migrationBuilder.DropTable(
                name: "LocationContents");

            migrationBuilder.DropTable(
                name: "LocationPaths");

            migrationBuilder.DropTable(
                name: "RequiredItems");

            migrationBuilder.DropTable(
                name: "Dialogs");

            migrationBuilder.DropTable(
                name: "InteractOptions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Interactibles");

            migrationBuilder.DropTable(
                name: "ItemCategories");
        }
    }
}
