using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class initintitinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndID",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "End",
                columns: table => new
                {
                    EndID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_End", x => x.EndID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_EndID",
                table: "Locations",
                column: "EndID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_End_EndID",
                table: "Locations",
                column: "EndID",
                principalTable: "End",
                principalColumn: "EndID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_End_EndID",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "End");

            migrationBuilder.DropIndex(
                name: "IX_Locations_EndID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "EndID",
                table: "Locations");
        }
    }
}
