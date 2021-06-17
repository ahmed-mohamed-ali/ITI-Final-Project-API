using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiveLife_API.Migrations
{
    public partial class updatecupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire",
                table: "Cupon");

            migrationBuilder.RenameColumn(
                name: "CuponValue",
                table: "Cupon",
                newName: "CuponIdentity");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategory",
                table: "Cupon",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfvalue",
                table: "Cupon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Cupon",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfvalue",
                table: "Cupon");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Cupon");

            migrationBuilder.RenameColumn(
                name: "CuponIdentity",
                table: "Cupon",
                newName: "CuponValue");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCategory",
                table: "Cupon",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "Expire",
                table: "Cupon",
                type: "bit",
                nullable: true);
        }
    }
}
