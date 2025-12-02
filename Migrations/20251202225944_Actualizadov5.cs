using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Actualizadov5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDevolucion",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "GeneradorSalida",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "ResponsableDevol",
                table: "Salidas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Salidas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneradorSalida",
                table: "Salidas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponsableDevol",
                table: "Salidas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
