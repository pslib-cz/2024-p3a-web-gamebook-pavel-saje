using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class End : Migration
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
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    DialogID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ends", x => x.EndID);
                    table.ForeignKey(
                        name: "FK_Ends_Dialogs_DialogID",
                        column: x => x.DialogID,
                        principalTable: "Dialogs",
                        principalColumn: "DialogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ends_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ends_DialogID",
                table: "Ends",
                column: "DialogID");

            migrationBuilder.CreateIndex(
                name: "IX_Ends_LocationID",
                table: "Ends",
                column: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ends");
        }
    }
}
