using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class updateDonnationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donnations",
                columns: table => new
                {
                    DonnationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonnationAmout = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegionCoordinatorId = table.Column<int>(type: "int", nullable: true),
                    OnlineDonnerId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donnations", x => x.DonnationID);
                    table.ForeignKey(
                        name: "FK_Donnations_OnlineDonner_OnlineDonnerId",
                        column: x => x.OnlineDonnerId,
                        principalTable: "OnlineDonner",
                        principalColumn: "DonnerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donnations_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donnations_RegionCoordinator_RegionCoordinatorId",
                        column: x => x.RegionCoordinatorId,
                        principalTable: "RegionCoordinator",
                        principalColumn: "CoordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donnations_OnlineDonnerId",
                table: "Donnations",
                column: "OnlineDonnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Donnations_RegionCoordinatorId",
                table: "Donnations",
                column: "RegionCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donnations_RegionId",
                table: "Donnations",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donnations");
        }
    }
}
