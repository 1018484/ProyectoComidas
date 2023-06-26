using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class PedidosPlato8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Pedidos_PedidosPedido_Id",
                table: "PedidosPlatos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosPlatos_PedidosPedido_Id",
                table: "PedidosPlatos");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPlatos_PlatosId",
                table: "PedidosPlatos",
                column: "PlatosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosPlatos_Platos_PlatosId",
                table: "PedidosPlatos",
                column: "PlatosId",
                principalTable: "Platos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Platos_PlatosId",
                table: "PedidosPlatos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosPlatos_PlatosId",
                table: "PedidosPlatos");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPlatos_PedidosPedido_Id",
                table: "PedidosPlatos",
                column: "PedidosPedido_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosPlatos_Pedidos_PedidosPedido_Id",
                table: "PedidosPlatos",
                column: "PedidosPedido_Id",
                principalTable: "Pedidos",
                principalColumn: "Pedido_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
