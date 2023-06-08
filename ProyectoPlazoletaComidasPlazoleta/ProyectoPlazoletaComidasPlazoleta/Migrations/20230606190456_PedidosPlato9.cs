using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class PedidosPlato9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Platos_PlatosId",
                table: "PedidosPlatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosPlatos",
                table: "PedidosPlatos");

            migrationBuilder.DropColumn(
                name: "PedidosPlatos_Id",
                table: "PedidosPlatos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "PedidosPlatos");

            migrationBuilder.RenameColumn(
                name: "PlatosId",
                table: "PedidosPlatos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PedidosPedido_Id",
                table: "PedidosPlatos",
                newName: "Pedido_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosPlatos_PlatosId",
                table: "PedidosPlatos",
                newName: "IX_PedidosPlatos_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosPlatos",
                table: "PedidosPlatos",
                columns: new[] { "Pedido_Id", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosPlatos_Pedidos_Pedido_Id",
                table: "PedidosPlatos",
                column: "Pedido_Id",
                principalTable: "Pedidos",
                principalColumn: "Pedido_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosPlatos_Platos_Id",
                table: "PedidosPlatos",
                column: "Id",
                principalTable: "Platos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Pedidos_Pedido_Id",
                table: "PedidosPlatos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosPlatos_Platos_Id",
                table: "PedidosPlatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosPlatos",
                table: "PedidosPlatos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PedidosPlatos",
                newName: "PlatosId");

            migrationBuilder.RenameColumn(
                name: "Pedido_Id",
                table: "PedidosPlatos",
                newName: "PedidosPedido_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosPlatos_Id",
                table: "PedidosPlatos",
                newName: "IX_PedidosPlatos_PlatosId");

            migrationBuilder.AddColumn<int>(
                name: "PedidosPlatos_Id",
                table: "PedidosPlatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "PedidosPlatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosPlatos",
                table: "PedidosPlatos",
                column: "PedidosPlatos_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosPlatos_Platos_PlatosId",
                table: "PedidosPlatos",
                column: "PlatosId",
                principalTable: "Platos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
