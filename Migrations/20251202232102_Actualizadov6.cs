using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Actualizadov6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContraseñaHash",
                table: "Usuarios",
                newName: "ContrasenaHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContrasenaHash",
                table: "Usuarios",
                newName: "ContraseñaHash");
        }
    }
}
