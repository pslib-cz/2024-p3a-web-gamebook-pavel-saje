using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class cigan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sell",
                columns: table => new
                {
                    SellID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    interactibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    itemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sell", x => x.SellID);
                    table.ForeignKey(
                        name: "FK_Sell_Interactibles_interactibleID",
                        column: x => x.interactibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sell_Items_itemID",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sell_interactibleID",
                table: "Sell",
                column: "interactibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_itemID",
                table: "Sell",
                column: "itemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sell");
        }
    }
}
