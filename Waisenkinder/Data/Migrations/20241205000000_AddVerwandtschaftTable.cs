using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITP2Tree.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVerwandtschaftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verwandtschaften",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonAId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonBId = table.Column<int>(type: "INTEGER", nullable: false),
                    Beziehungstyp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verwandtschaften", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verwandtschaften_Personen_PersonAId",
                        column: x => x.PersonAId,
                        principalTable: "Personen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verwandtschaften_Personen_PersonBId",
                        column: x => x.PersonBId,
                        principalTable: "Personen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Verwandtschaften_PersonAId",
                table: "Verwandtschaften",
                column: "PersonAId");

            migrationBuilder.CreateIndex(
                name: "IX_Verwandtschaften_PersonBId",
                table: "Verwandtschaften",
                column: "PersonBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verwandtschaften");
        }
    }
}
