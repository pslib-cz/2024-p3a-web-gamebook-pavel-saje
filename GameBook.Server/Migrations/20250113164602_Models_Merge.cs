using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class Models_Merge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interactibles",
                columns: table => new
                {
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactibles", x => x.InteractibleID);
                });

            migrationBuilder.CreateTable(
                name: "InteractOptions",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractOptions", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BackgroundImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    RadiationGain = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Dialogs",
                columns: table => new
                {
                    DialogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    DialogOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogs", x => x.DialogID);
                    table.ForeignKey(
                        name: "FK_Dialogs_Interactibles_IteractibleID",
                        column: x => x.IteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractiblesOptions",
                columns: table => new
                {
                    InteractibleOptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiblesOptions", x => x.InteractibleOptionID);
                    table.ForeignKey(
                        name: "FK_InteractiblesOptions_InteractOptions_OptionID",
                        column: x => x.OptionID,
                        principalTable: "InteractOptions",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractiblesOptions_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TradeValue = table.Column<int>(type: "INTEGER", nullable: true),
                    Stackable = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationContent",
                columns: table => new
                {
                    LocationContentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    XPos = table.Column<int>(type: "INTEGER", nullable: false),
                    YPos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationContent", x => x.LocationContentID);
                    table.ForeignKey(
                        name: "FK_LocationContent_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationContent_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationPaths",
                columns: table => new
                {
                    PathID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstNodeID = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondNodeID = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyTravelCost = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPaths", x => x.PathID);
                    table.ForeignKey(
                        name: "FK_LocationPaths_Locations_FirstNodeID",
                        column: x => x.FirstNodeID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationPaths_Locations_SecondNodeID",
                        column: x => x.SecondNodeID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogResponses",
                columns: table => new
                {
                    DialogResponseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DialogID = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponseText = table.Column<string>(type: "TEXT", nullable: false),
                    RelationshipEffect = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogResponses", x => x.DialogResponseID);
                    table.ForeignKey(
                        name: "FK_DialogResponses_Dialogs_DialogID",
                        column: x => x.DialogID,
                        principalTable: "Dialogs",
                        principalColumn: "DialogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableItems",
                columns: table => new
                {
                    ConsumableItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    HealthValue = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyValue = table.Column<int>(type: "INTEGER", nullable: false),
                    RadiationValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableItems", x => x.ConsumableItemID);
                    table.ForeignKey(
                        name: "FK_ConsumableItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractiblesItems",
                columns: table => new
                {
                    InteractibleItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiblesItems", x => x.InteractibleItemID);
                    table.ForeignKey(
                        name: "FK_InteractiblesItems_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractiblesItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredItems",
                columns: table => new
                {
                    RequiredItemsID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredItems", x => x.RequiredItemsID);
                    table.ForeignKey(
                        name: "FK_RequiredItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredItems_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableItems_ItemID",
                table: "ConsumableItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DialogResponses_DialogID",
                table: "DialogResponses",
                column: "DialogID");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_IteractibleID",
                table: "Dialogs",
                column: "IteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesItems_InteractibleID",
                table: "InteractiblesItems",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesItems_ItemId",
                table: "InteractiblesItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesOptions_InteractibleID",
                table: "InteractiblesOptions",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesOptions_OptionID",
                table: "InteractiblesOptions",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContent_InteractibleID",
                table: "LocationContent",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContent_LocationID",
                table: "LocationContent",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPaths_FirstNodeID",
                table: "LocationPaths",
                column: "FirstNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPaths_SecondNodeID",
                table: "LocationPaths",
                column: "SecondNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItems_ItemID",
                table: "RequiredItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItems_LocationID",
                table: "RequiredItems",
                column: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumableItems");

            migrationBuilder.DropTable(
                name: "DialogResponses");

            migrationBuilder.DropTable(
                name: "InteractiblesItems");

            migrationBuilder.DropTable(
                name: "InteractiblesOptions");

            migrationBuilder.DropTable(
                name: "LocationContent");

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
