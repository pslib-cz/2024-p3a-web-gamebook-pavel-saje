using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class tradeInteractible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeInteractibles",
                columns: table => new
                {
                    TradeInteractibleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    TradeValue = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeInteractibles", x => x.TradeInteractibleID);
                    table.ForeignKey(
                        name: "FK_TradeInteractibles_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeInteractibles_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradesInteractibles",
                columns: table => new
                {
                    TradesInteractibleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    TradeInteractibleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradesInteractibles", x => x.TradesInteractibleID);
                    table.ForeignKey(
                        name: "FK_TradesInteractibles_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradesInteractibles_TradeInteractibles_TradeInteractibleID",
                        column: x => x.TradeInteractibleID,
                        principalTable: "TradeInteractibles",
                        principalColumn: "TradeInteractibleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeInteractibles_InteractibleID",
                table: "TradeInteractibles",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeInteractibles_ItemID",
                table: "TradeInteractibles",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TradesInteractibles_InteractibleID",
                table: "TradesInteractibles",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_TradesInteractibles_TradeInteractibleID",
                table: "TradesInteractibles",
                column: "TradeInteractibleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradesInteractibles");

            migrationBuilder.DropTable(
                name: "TradeInteractibles");
        }
    }
}
