using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApi.Migrations
{
    public partial class AjusteNombre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaNacimineto",
                table: "Actores",
                newName: "FechaNacimiento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Actores",
                newName: "FechaNacimineto");
        }
    }
}
