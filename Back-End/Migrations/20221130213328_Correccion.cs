using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApi.Migrations
{
    public partial class Correccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PeliculasCines");

            migrationBuilder.DropColumn(
                name: "Personaje",
                table: "PeliculasCines");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "PeliculasActores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Personaje",
                table: "PeliculasActores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "PeliculasActores");

            migrationBuilder.DropColumn(
                name: "Personaje",
                table: "PeliculasActores");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PeliculasCines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Personaje",
                table: "PeliculasCines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
