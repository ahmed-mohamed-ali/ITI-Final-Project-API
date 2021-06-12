using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class DonnationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "OnlineDonner",
                columns: table => new
                {
                    DonnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletBalance = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    VisaNum = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineDonner", x => x.DonnerID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionID = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    NationalID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Family_Member_Num = table.Column<int>(type: "int", nullable: true),
                    Child_Num = table.Column<int>(type: "int", nullable: true),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, defaultValueSql: "(N'pending')"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.NationalID);
                    table.ForeignKey(
                        name: "FK_Cases_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActiveType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WalletBalance = table.Column<decimal>(type: "money", nullable: true),
                    VisaNum = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrgID);
                    table.ForeignKey(
                        name: "FK_Organization_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionAdmin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankAccountBalance = table.Column<decimal>(type: "money", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionAdmin", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_RegionAdmin_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Group",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Group", x => new { x.UserID, x.GroupID });
                    table.ForeignKey(
                        name: "FK_User_Group_Group",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Group_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionCoordinator",
                columns: table => new
                {
                    CoordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    VisaNum = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    WalletBalance = table.Column<decimal>(type: "money", nullable: true),
                    RegionAdminID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionCoordinator", x => x.CoordID);
                    table.ForeignKey(
                        name: "FK_RegionCoordinator_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegionCoordinator_RegionAdmin1",
                        column: x => x.RegionAdminID,
                        principalTable: "RegionAdmin",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cupon",
                columns: table => new
                {
                    CuponID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuponValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Expire = table.Column<bool>(type: "bit", nullable: true),
                    CaseNationalID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    CoordID = table.Column<int>(type: "int", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupon", x => x.CuponID);
                    table.ForeignKey(
                        name: "FK_Cupon_Cases",
                        column: x => x.CaseNationalID,
                        principalTable: "Cases",
                        principalColumn: "NationalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cupon_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cupon_RegionCoordinator1",
                        column: x => x.CoordID,
                        principalTable: "RegionCoordinator",
                        principalColumn: "CoordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordID = table.Column<int>(type: "int", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    PostTxt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CaseId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "money", nullable: true),
                    RestAmount = table.Column<decimal>(type: "money", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(N'pending')"),
                    NeedCatogry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    OrgID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Post_Cases",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "NationalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_Organization",
                        column: x => x.OrgID,
                        principalTable: "Organization",
                        principalColumn: "OrgID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_RegionCoordinator",
                        column: x => x.CoordID,
                        principalTable: "RegionCoordinator",
                        principalColumn: "CoordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_RegionID",
                table: "Cases",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Cupon_CaseNationalID",
                table: "Cupon",
                column: "CaseNationalID");

            migrationBuilder.CreateIndex(
                name: "IX_Cupon_CoordID",
                table: "Cupon",
                column: "CoordID");

            migrationBuilder.CreateIndex(
                name: "IX_Cupon_RegionID",
                table: "Cupon",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_RegionID",
                table: "Organization",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CaseId",
                table: "Post",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CoordID",
                table: "Post",
                column: "CoordID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_OrgID",
                table: "Post",
                column: "OrgID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_RegionID",
                table: "Post",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionAdmin_RegionID",
                table: "RegionAdmin",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCoordinator_RegionAdminID",
                table: "RegionCoordinator",
                column: "RegionAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCoordinator_RegionID",
                table: "RegionCoordinator",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Group_GroupID",
                table: "User_Group",
                column: "GroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupon");

            migrationBuilder.DropTable(
                name: "OnlineDonner");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "User_Group");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "RegionCoordinator");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RegionAdmin");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
