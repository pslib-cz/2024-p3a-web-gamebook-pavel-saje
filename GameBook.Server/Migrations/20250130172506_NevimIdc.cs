using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class NevimIdc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableItem_Item_ItemID",
                table: "ConsumableItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DataDialog_DataDialog_NextDialogID",
                table: "DataDialog");

            migrationBuilder.DropForeignKey(
                name: "FK_DataDialog_Interactible_IteractibleID",
                table: "DataDialog");

            migrationBuilder.DropForeignKey(
                name: "FK_DataDialogResponse_DataDialog_DialogID",
                table: "DataDialogResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_DataDialogResponse_DataDialog_NextDialogID",
                table: "DataDialogResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesItem_Interactible_InteractibleID",
                table: "InteractiblesItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesItem_Item_ItemId",
                table: "InteractiblesItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesOption_InteractOption_OptionID",
                table: "InteractiblesOption");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesOption_Interactible_InteractibleID",
                table: "InteractiblesOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemCategory_CategoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationContent_Interactible_InteractibleID",
                table: "LocationContent");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationContent_Location_LocationID",
                table: "LocationContent");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPath_Location_FirstNodeID",
                table: "LocationPath");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPath_Location_SecondNodeID",
                table: "LocationPath");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredItems_Item_ItemID",
                table: "RequiredItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredItems_Location_LocationID",
                table: "RequiredItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationPath",
                table: "LocationPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationContent",
                table: "LocationContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCategory",
                table: "ItemCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractOption",
                table: "InteractOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractiblesOption",
                table: "InteractiblesOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractiblesItem",
                table: "InteractiblesItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interactible",
                table: "Interactible");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataDialogResponse",
                table: "DataDialogResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataDialog",
                table: "DataDialog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumableItem",
                table: "ConsumableItem");

            migrationBuilder.RenameTable(
                name: "LocationPath",
                newName: "LocationPaths");

            migrationBuilder.RenameTable(
                name: "LocationContent",
                newName: "LocationContents");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "ItemCategory",
                newName: "ItemCategories");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "InteractOption",
                newName: "InteractOptions");

            migrationBuilder.RenameTable(
                name: "InteractiblesOption",
                newName: "InteractiblesOptions");

            migrationBuilder.RenameTable(
                name: "InteractiblesItem",
                newName: "InteractiblesItems");

            migrationBuilder.RenameTable(
                name: "Interactible",
                newName: "Interactibles");

            migrationBuilder.RenameTable(
                name: "DataDialogResponse",
                newName: "DialogResponses");

            migrationBuilder.RenameTable(
                name: "DataDialog",
                newName: "Dialogs");

            migrationBuilder.RenameTable(
                name: "ConsumableItem",
                newName: "ConsumableItems");

            migrationBuilder.RenameIndex(
                name: "IX_LocationPath_SecondNodeID",
                table: "LocationPaths",
                newName: "IX_LocationPaths_SecondNodeID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationPath_FirstNodeID",
                table: "LocationPaths",
                newName: "IX_LocationPaths_FirstNodeID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationContent_LocationID",
                table: "LocationContents",
                newName: "IX_LocationContents_LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationContent_InteractibleID",
                table: "LocationContents",
                newName: "IX_LocationContents_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CategoryId",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesOption_OptionID",
                table: "InteractiblesOptions",
                newName: "IX_InteractiblesOptions_OptionID");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesOption_InteractibleID",
                table: "InteractiblesOptions",
                newName: "IX_InteractiblesOptions_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesItem_ItemId",
                table: "InteractiblesItems",
                newName: "IX_InteractiblesItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesItem_InteractibleID",
                table: "InteractiblesItems",
                newName: "IX_InteractiblesItems_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_DataDialogResponse_NextDialogID",
                table: "DialogResponses",
                newName: "IX_DialogResponses_NextDialogID");

            migrationBuilder.RenameIndex(
                name: "IX_DataDialogResponse_DialogID",
                table: "DialogResponses",
                newName: "IX_DialogResponses_DialogID");

            migrationBuilder.RenameIndex(
                name: "IX_DataDialog_NextDialogID",
                table: "Dialogs",
                newName: "IX_Dialogs_NextDialogID");

            migrationBuilder.RenameIndex(
                name: "IX_DataDialog_IteractibleID",
                table: "Dialogs",
                newName: "IX_Dialogs_IteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumableItem_ItemID",
                table: "ConsumableItems",
                newName: "IX_ConsumableItems_ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationPaths",
                table: "LocationPaths",
                column: "PathID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationContents",
                table: "LocationContents",
                column: "LocationContentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "LocationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractOptions",
                table: "InteractOptions",
                column: "OptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractiblesOptions",
                table: "InteractiblesOptions",
                column: "InteractiblesOptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractiblesItems",
                table: "InteractiblesItems",
                column: "InteractiblesItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interactibles",
                table: "Interactibles",
                column: "InteractibleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DialogResponses",
                table: "DialogResponses",
                column: "DialogResponseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dialogs",
                table: "Dialogs",
                column: "DialogID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumableItems",
                table: "ConsumableItems",
                column: "ConsumableItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableItems_Items_ItemID",
                table: "ConsumableItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DialogResponses_Dialogs_DialogID",
                table: "DialogResponses",
                column: "DialogID",
                principalTable: "Dialogs",
                principalColumn: "DialogID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DialogResponses_Dialogs_NextDialogID",
                table: "DialogResponses",
                column: "NextDialogID",
                principalTable: "Dialogs",
                principalColumn: "DialogID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Dialogs_NextDialogID",
                table: "Dialogs",
                column: "NextDialogID",
                principalTable: "Dialogs",
                principalColumn: "DialogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Interactibles_IteractibleID",
                table: "Dialogs",
                column: "IteractibleID",
                principalTable: "Interactibles",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesItems_Interactibles_InteractibleID",
                table: "InteractiblesItems",
                column: "InteractibleID",
                principalTable: "Interactibles",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesItems_Items_ItemId",
                table: "InteractiblesItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesOptions_InteractOptions_OptionID",
                table: "InteractiblesOptions",
                column: "OptionID",
                principalTable: "InteractOptions",
                principalColumn: "OptionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesOptions_Interactibles_InteractibleID",
                table: "InteractiblesOptions",
                column: "InteractibleID",
                principalTable: "Interactibles",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationContents_Interactibles_InteractibleID",
                table: "LocationContents",
                column: "InteractibleID",
                principalTable: "Interactibles",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationContents_Locations_LocationID",
                table: "LocationContents",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPaths_Locations_FirstNodeID",
                table: "LocationPaths",
                column: "FirstNodeID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPaths_Locations_SecondNodeID",
                table: "LocationPaths",
                column: "SecondNodeID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredItems_Items_ItemID",
                table: "RequiredItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredItems_Locations_LocationID",
                table: "RequiredItems",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableItems_Items_ItemID",
                table: "ConsumableItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DialogResponses_Dialogs_DialogID",
                table: "DialogResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_DialogResponses_Dialogs_NextDialogID",
                table: "DialogResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Dialogs_NextDialogID",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Interactibles_IteractibleID",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesItems_Interactibles_InteractibleID",
                table: "InteractiblesItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesItems_Items_ItemId",
                table: "InteractiblesItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesOptions_InteractOptions_OptionID",
                table: "InteractiblesOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractiblesOptions_Interactibles_InteractibleID",
                table: "InteractiblesOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationContents_Interactibles_InteractibleID",
                table: "LocationContents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationContents_Locations_LocationID",
                table: "LocationContents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPaths_Locations_FirstNodeID",
                table: "LocationPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPaths_Locations_SecondNodeID",
                table: "LocationPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredItems_Items_ItemID",
                table: "RequiredItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredItems_Locations_LocationID",
                table: "RequiredItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationPaths",
                table: "LocationPaths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationContents",
                table: "LocationContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractOptions",
                table: "InteractOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractiblesOptions",
                table: "InteractiblesOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractiblesItems",
                table: "InteractiblesItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interactibles",
                table: "Interactibles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dialogs",
                table: "Dialogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DialogResponses",
                table: "DialogResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumableItems",
                table: "ConsumableItems");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "LocationPaths",
                newName: "LocationPath");

            migrationBuilder.RenameTable(
                name: "LocationContents",
                newName: "LocationContent");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "ItemCategories",
                newName: "ItemCategory");

            migrationBuilder.RenameTable(
                name: "InteractOptions",
                newName: "InteractOption");

            migrationBuilder.RenameTable(
                name: "InteractiblesOptions",
                newName: "InteractiblesOption");

            migrationBuilder.RenameTable(
                name: "InteractiblesItems",
                newName: "InteractiblesItem");

            migrationBuilder.RenameTable(
                name: "Interactibles",
                newName: "Interactible");

            migrationBuilder.RenameTable(
                name: "Dialogs",
                newName: "DataDialog");

            migrationBuilder.RenameTable(
                name: "DialogResponses",
                newName: "DataDialogResponse");

            migrationBuilder.RenameTable(
                name: "ConsumableItems",
                newName: "ConsumableItem");

            migrationBuilder.RenameIndex(
                name: "IX_LocationPaths_SecondNodeID",
                table: "LocationPath",
                newName: "IX_LocationPath_SecondNodeID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationPaths_FirstNodeID",
                table: "LocationPath",
                newName: "IX_LocationPath_FirstNodeID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationContents_LocationID",
                table: "LocationContent",
                newName: "IX_LocationContent_LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_LocationContents_InteractibleID",
                table: "LocationContent",
                newName: "IX_LocationContent_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Item",
                newName: "IX_Item_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesOptions_OptionID",
                table: "InteractiblesOption",
                newName: "IX_InteractiblesOption_OptionID");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesOptions_InteractibleID",
                table: "InteractiblesOption",
                newName: "IX_InteractiblesOption_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesItems_ItemId",
                table: "InteractiblesItem",
                newName: "IX_InteractiblesItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractiblesItems_InteractibleID",
                table: "InteractiblesItem",
                newName: "IX_InteractiblesItem_InteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogs_NextDialogID",
                table: "DataDialog",
                newName: "IX_DataDialog_NextDialogID");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogs_IteractibleID",
                table: "DataDialog",
                newName: "IX_DataDialog_IteractibleID");

            migrationBuilder.RenameIndex(
                name: "IX_DialogResponses_NextDialogID",
                table: "DataDialogResponse",
                newName: "IX_DataDialogResponse_NextDialogID");

            migrationBuilder.RenameIndex(
                name: "IX_DialogResponses_DialogID",
                table: "DataDialogResponse",
                newName: "IX_DataDialogResponse_DialogID");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumableItems_ItemID",
                table: "ConsumableItem",
                newName: "IX_ConsumableItem_ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "LocationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationPath",
                table: "LocationPath",
                column: "PathID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationContent",
                table: "LocationContent",
                column: "LocationContentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCategory",
                table: "ItemCategory",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractOption",
                table: "InteractOption",
                column: "OptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractiblesOption",
                table: "InteractiblesOption",
                column: "InteractiblesOptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractiblesItem",
                table: "InteractiblesItem",
                column: "InteractiblesItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interactible",
                table: "Interactible",
                column: "InteractibleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataDialog",
                table: "DataDialog",
                column: "DialogID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataDialogResponse",
                table: "DataDialogResponse",
                column: "DialogResponseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumableItem",
                table: "ConsumableItem",
                column: "ConsumableItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableItem_Item_ItemID",
                table: "ConsumableItem",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataDialog_DataDialog_NextDialogID",
                table: "DataDialog",
                column: "NextDialogID",
                principalTable: "DataDialog",
                principalColumn: "DialogID");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDialog_Interactible_IteractibleID",
                table: "DataDialog",
                column: "IteractibleID",
                principalTable: "Interactible",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataDialogResponse_DataDialog_DialogID",
                table: "DataDialogResponse",
                column: "DialogID",
                principalTable: "DataDialog",
                principalColumn: "DialogID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataDialogResponse_DataDialog_NextDialogID",
                table: "DataDialogResponse",
                column: "NextDialogID",
                principalTable: "DataDialog",
                principalColumn: "DialogID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesItem_Interactible_InteractibleID",
                table: "InteractiblesItem",
                column: "InteractibleID",
                principalTable: "Interactible",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesItem_Item_ItemId",
                table: "InteractiblesItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesOption_InteractOption_OptionID",
                table: "InteractiblesOption",
                column: "OptionID",
                principalTable: "InteractOption",
                principalColumn: "OptionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractiblesOption_Interactible_InteractibleID",
                table: "InteractiblesOption",
                column: "InteractibleID",
                principalTable: "Interactible",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemCategory_CategoryId",
                table: "Item",
                column: "CategoryId",
                principalTable: "ItemCategory",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationContent_Interactible_InteractibleID",
                table: "LocationContent",
                column: "InteractibleID",
                principalTable: "Interactible",
                principalColumn: "InteractibleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationContent_Location_LocationID",
                table: "LocationContent",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPath_Location_FirstNodeID",
                table: "LocationPath",
                column: "FirstNodeID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPath_Location_SecondNodeID",
                table: "LocationPath",
                column: "SecondNodeID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredItems_Item_ItemID",
                table: "RequiredItems",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredItems_Location_LocationID",
                table: "RequiredItems",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
