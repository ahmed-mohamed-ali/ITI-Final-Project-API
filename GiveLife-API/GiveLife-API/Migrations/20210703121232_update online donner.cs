using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class updateonlinedonner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OnlineDonner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OnlineDonner");
        }
    }
}
