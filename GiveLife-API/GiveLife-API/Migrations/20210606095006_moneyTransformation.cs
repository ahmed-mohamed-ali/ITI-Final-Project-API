using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class moneyTransformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyTransformation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionAdminId = table.Column<int>(type: "int", nullable: false),
                    RegionCoordinatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransformation", x => x.id);
                    table.ForeignKey(
                        name: "FK_MoneyTransformation_RegionAdmin_RegionAdminId",
                        column: x => x.RegionAdminId,
                        principalTable: "RegionAdmin",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyTransformation_RegionCoordinator_RegionCoordinatorId",
                        column: x => x.RegionCoordinatorId,
                        principalTable: "RegionCoordinator",
                        principalColumn: "CoordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransformation_RegionAdminId",
                table: "MoneyTransformation",
                column: "RegionAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransformation_RegionCoordinatorId",
                table: "MoneyTransformation",
                column: "RegionCoordinatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransformation");
        }
    }
}
