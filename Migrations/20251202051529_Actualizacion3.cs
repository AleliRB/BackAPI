using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesSalidas_Productos_IdProducto",
                table: "DetallesSalidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_TiposEmpleado_IdTipEmp",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Empleados_IdEmp",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_IdEmp",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesSalidas",
                table: "DetallesSalidas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesSalidas",
                table: "DetallesSalidas",
                columns: new[] { "IdSalida", "IdProducto" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "IdCat", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Electrónica" },
                    { 2, "Ropa" },
                    { 3, "Alimentos" },
                    { 4, "Juguetes" }
                });

            migrationBuilder.InsertData(
                table: "TiposEmpleado",
                columns: new[] { "IdTipEmp", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Secretario" },
                    { 3, "Almacenero" },
                    { 4, "Vendedor" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesSalidas_Productos_IdProducto",
                table: "DetallesSalidas",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_TiposEmpleado_IdTipEmp",
                table: "Empleados",
                column: "IdTipEmp",
                principalTable: "TiposEmpleado",
                principalColumn: "IdTipEmp",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCat",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Empleados_IdEmp",
                table: "Salidas",
                column: "IdEmp",
                principalTable: "Empleados",
                principalColumn: "IdEmp",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_IdEmp",
                table: "Usuarios",
                column: "IdEmp",
                principalTable: "Empleados",
                principalColumn: "IdEmp",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesSalidas_Productos_IdProducto",
                table: "DetallesSalidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_TiposEmpleado_IdTipEmp",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Empleados_IdEmp",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_IdEmp",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesSalidas",
                table: "DetallesSalidas");

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "IdCat",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "IdCat",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "IdCat",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "IdCat",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TiposEmpleado",
                keyColumn: "IdTipEmp",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposEmpleado",
                keyColumn: "IdTipEmp",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposEmpleado",
                keyColumn: "IdTipEmp",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TiposEmpleado",
                keyColumn: "IdTipEmp",
                keyValue: 4);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesSalidas",
                table: "DetallesSalidas",
                column: "IdSalida");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesSalidas_Productos_IdProducto",
                table: "DetallesSalidas",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_TiposEmpleado_IdTipEmp",
                table: "Empleados",
                column: "IdTipEmp",
                principalTable: "TiposEmpleado",
                principalColumn: "IdTipEmp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCat",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Empleados_IdEmp",
                table: "Salidas",
                column: "IdEmp",
                principalTable: "Empleados",
                principalColumn: "IdEmp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_IdEmp",
                table: "Usuarios",
                column: "IdEmp",
                principalTable: "Empleados",
                principalColumn: "IdEmp",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
