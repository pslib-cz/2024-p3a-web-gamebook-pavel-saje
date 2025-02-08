using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class NpcsNWeapons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    WeaponID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.WeaponID);
                });

            migrationBuilder.CreateTable(
                name: "Npcs",
                columns: table => new
                {
                    NpcID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Health = table.Column<int>(type: "INTEGER", nullable: true),
                    WeaponID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Npcs", x => x.NpcID);
                    table.ForeignKey(
                        name: "FK_Npcs_Weapons_WeaponID",
                        column: x => x.WeaponID,
                        principalTable: "Weapons",
                        principalColumn: "WeaponID");
                });

            migrationBuilder.CreateTable(
                name: "InteractiblesNpcs",
                columns: table => new
                {
                    InteractiblesNpcID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractibleID = table.Column<int>(type: "INTEGER", nullable: false),
                    NpcID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiblesNpcs", x => x.InteractiblesNpcID);
                    table.ForeignKey(
                        name: "FK_InteractiblesNpcs_Interactibles_InteractibleID",
                        column: x => x.InteractibleID,
                        principalTable: "Interactibles",
                        principalColumn: "InteractibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractiblesNpcs_Npcs_NpcID",
                        column: x => x.NpcID,
                        principalTable: "Npcs",
                        principalColumn: "NpcID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesNpcs_InteractibleID",
                table: "InteractiblesNpcs",
                column: "InteractibleID");

            migrationBuilder.CreateIndex(
                name: "IX_InteractiblesNpcs_NpcID",
                table: "InteractiblesNpcs",
                column: "NpcID");

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_WeaponID",
                table: "Npcs",
                column: "WeaponID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InteractiblesNpcs");

            migrationBuilder.DropTable(
                name: "Npcs");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
