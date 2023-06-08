using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class PedidosPlato6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosPlatos",
                columns: table => new
                {
                    PedidosPlatos_Id = table.Column<int>(type: "int", nullable: false),
                    PedidosPedido_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatosId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosPlatos", x => x.PedidosPlatos_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosPlatos");
        }
    }
}
