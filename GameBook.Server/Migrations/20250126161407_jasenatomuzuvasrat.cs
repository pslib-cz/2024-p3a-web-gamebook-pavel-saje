using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class jasenatomuzuvasrat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interactible",
                columns: table => new
                {
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactible", x => x.InteractibleID);
                });

            migrationBuilder.CreateTable(
                name: "InteractOption",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractOption", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BackgroundImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    RadiationGain = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "DataDialog",
                columns: table => new
                {
                    DialogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    NextDialogID = table.Column<int>(type: "INTEGER", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDialog", x => x.DialogID);
                    table.ForeignKey(
                        name: "FK_DataDialog_DataDialog_NextDialogID",
                        column: x => x.NextDialogID,
                        principalTable: "DataDialog",
                        principalColumn: "DialogID");
                    table.ForeignKey(
                        name: "FK_DataDialog_Interactible_IteractibleID",
                        column: x => x.IteractibleID,
                        principalTable: "Interactible",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractiblesOption",
                columns: table => new
                {
                    InteractiblesOptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiblesOption", x => x.InteractiblesOptionID);
                    table.ForeignKey(
                        name: "FK_InteractiblesOption_InteractOption_OptionID",
                        column: x => x.OptionID,
                        principalTable: "InteractOption",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractiblesOption_Interactible_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactible",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
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
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Item_ItemCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategory",
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
                        name: "FK_LocationContent_Interactible_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactible",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationContent_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationPath",
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
                    table.PrimaryKey("PK_LocationPath", x => x.PathID);
                    table.ForeignKey(
                        name: "FK_LocationPath_Location_FirstNodeID",
                        column: x => x.FirstNodeID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationPath_Location_SecondNodeID",
                        column: x => x.SecondNodeID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataDialogResponse",
                columns: table => new
                {
                    DialogResponseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DialogID = table.Column<int>(type: "INTEGER", nullable: false),
                    NextDialogID = table.Column<int>(type: "INTEGER", nullable: true),
                    ResponseText = table.Column<string>(type: "TEXT", nullable: false),
                    RelationshipEffect = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDialogResponse", x => x.DialogResponseID);
                    table.ForeignKey(
                        name: "FK_DataDialogResponse_DataDialog_DialogID",
                        column: x => x.DialogID,
                        principalTable: "DataDialog",
                        principalColumn: "DialogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataDialogResponse_DataDialog_NextDialogID",
                        column: x => x.NextDialogID,
                        principalTable: "DataDialog",
                        principalColumn: "DialogID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableItem",
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
                    table.PrimaryKey("PK_ConsumableItem", x => x.ConsumableItemID);
                    table.ForeignKey(
                        name: "FK_ConsumableItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractiblesItem",
                columns: table => new
                {
                    InteractiblesItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiblesItem", x => x.InteractiblesItemID);
                    table.ForeignKey(
                        name: "FK_InteractiblesItem_Interactible_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactible",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractiblesItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
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
                        name: "FK_RequiredItems_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredItems_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableItem_ItemID",
                table: "ConsumableItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DataDialog_IteractibleID",
                table: "DataDialog",
                column: "IteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_DataDialog_NextDialogID",
                table: "DataDialog",
                column: "NextDialogID");

            migrationBuilder.CreateIndex(
                name: "IX_DataDialogResponse_DialogID",
                table: "DataDialogResponse",
                column: "DialogID");

            migrationBuilder.CreateIndex(
                name: "IX_DataDialogResponse_NextDialogID",
                table: "DataDialogResponse",
                column: "NextDialogID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesItem_InteractibleID",
                table: "InteractiblesItem",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesItem_ItemId",
                table: "InteractiblesItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesOption_InteractibleID",
                table: "InteractiblesOption",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesOption_OptionID",
                table: "InteractiblesOption",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
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
                name: "IX_LocationPath_FirstNodeID",
                table: "LocationPath",
                column: "FirstNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPath_SecondNodeID",
                table: "LocationPath",
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
                name: "ConsumableItem");

            migrationBuilder.DropTable(
                name: "DataDialogResponse");

            migrationBuilder.DropTable(
                name: "InteractiblesItem");

            migrationBuilder.DropTable(
                name: "InteractiblesOption");

            migrationBuilder.DropTable(
                name: "LocationContent");

            migrationBuilder.DropTable(
                name: "LocationPath");

            migrationBuilder.DropTable(
                name: "RequiredItems");

            migrationBuilder.DropTable(
                name: "DataDialog");

            migrationBuilder.DropTable(
                name: "InteractOption");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Interactible");

            migrationBuilder.DropTable(
                name: "ItemCategory");
        }
    }
}
