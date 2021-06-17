using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class casestatusenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Cases",
                type: "int",
                maxLength: 15,
                nullable: false,
                defaultValueSql: "(N'pending')",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true,
                oldDefaultValueSql: "(N'pending')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Cases",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                defaultValueSql: "(N'pending')",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 15,
                oldDefaultValueSql: "(N'pending')");
        }
    }
}
