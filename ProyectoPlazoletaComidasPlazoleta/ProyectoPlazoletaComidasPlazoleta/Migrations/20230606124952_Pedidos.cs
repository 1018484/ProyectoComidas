using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class Pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurantes",
                columns: table => new
                {
                    NIT_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URLLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantes", x => x.NIT_Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Pedido_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cliente_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chef_Id = table.Column<int>(type: "int", nullable: false),
                    RestaurantesNIT_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Pedido_Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Restaurantes_RestaurantesNIT_Id",
                        column: x => x.RestaurantesNIT_Id,
                        principalTable: "Restaurantes",
                        principalColumn: "NIT_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePlato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Desacripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URLImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantesNIT_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Platos_Restaurantes_RestaurantesNIT_Id",
                        column: x => x.RestaurantesNIT_Id,
                        principalTable: "Restaurantes",
                        principalColumn: "NIT_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_RestaurantesNIT_Id",
                table: "Pedidos",
                column: "RestaurantesNIT_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Platos_RestaurantesNIT_Id",
                table: "Platos",
                column: "RestaurantesNIT_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Platos");

            migrationBuilder.DropTable(
                name: "Restaurantes");
        }
    }
}
