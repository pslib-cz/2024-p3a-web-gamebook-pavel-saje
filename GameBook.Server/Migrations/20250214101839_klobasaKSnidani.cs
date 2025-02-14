using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class klobasaKSnidani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_ItemID",
                table: "Weapons",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Items_ItemID",
                table: "Weapons",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Items_ItemID",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_ItemID",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "Weapons");
        }
    }
}
