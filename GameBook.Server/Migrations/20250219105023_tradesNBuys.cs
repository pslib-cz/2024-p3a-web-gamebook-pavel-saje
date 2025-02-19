using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class tradesNBuys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buy",
                columns: table => new
                {
                    BuyID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buy", x => x.BuyID);
                    table.ForeignKey(
                        name: "FK_Buy_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buy_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Item1ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Item2ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeID);
                    table.ForeignKey(
                        name: "FK_Trade_Items_Item1ID",
                        column: x => x.Item1ID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trade_Items_Item2ID",
                        column: x => x.Item2ID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    TradesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    interactibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    tradeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.TradesID);
                    table.ForeignKey(
                        name: "FK_Trades_Interactibles_interactibleID",
                        column: x => x.interactibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trades_Trade_tradeID",
                        column: x => x.tradeID,
                        principalTable: "Trade",
                        principalColumn: "TradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buy_InteractibleID",
                table: "Buy",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Buy_ItemID",
                table: "Buy",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_Item1ID",
                table: "Trade",
                column: "Item1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_Item2ID",
                table: "Trade",
                column: "Item2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_interactibleID",
                table: "Trades",
                column: "interactibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_tradeID",
                table: "Trades",
                column: "tradeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buy");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Trade");
        }
    }
}
