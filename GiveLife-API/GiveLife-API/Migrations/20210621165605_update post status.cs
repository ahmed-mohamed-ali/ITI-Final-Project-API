using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class updatepoststatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Post",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "(N'pending')",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "(N'pending')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Post",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "(N'pending')",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldDefaultValueSql: "(N'pending')");
        }
    }
}
