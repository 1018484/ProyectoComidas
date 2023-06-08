using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    public partial class PedidosPlato11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");
        }
    }
}
