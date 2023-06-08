using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class PedidosPlato7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Pedidos_PedidosPedido_Id",
                table: "PedidosPlatos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosPlatos_PedidosPedido_Id",
                table: "PedidosPlatos");
        }
    }
}
