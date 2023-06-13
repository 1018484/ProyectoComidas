using Aplicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IOrdersRepository<Pedidos, string> repoOrders;

        private readonly IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> repoEmployeeRestaurant;

        private readonly IRoles repoRoles;

        private Task<UsuarioClaims> getClaims;

        public EmployeeService(IOrdersRepository<Pedidos, string> Orders, IRoles Roles, IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> employeeRestaurant)
        {
            this.repoOrders = Orders;
            this.repoRoles = Roles;
            this.repoEmployeeRestaurant = employeeRestaurant;
            this.getClaims = this.repoRoles.getToken();
        }

        public void AssignOrder(List<Guid> orders)
        {
            if (Enum.Parse<EnumRoles>(getClaims.Result.Rol) != EnumRoles.Empleado)
            {
                throw new Exception("User Not authorized");
            }
            foreach (var order in orders)
            {
                repoOrders.Update(order, int.Parse(getClaims.Result.Id));
            }            
        }

        public async Task<List<Pedidos>>ListOrders(PedidsoFiltroDTO filter)
        {
            int paginas = 0;           
            if (Enum.Parse<EnumRoles>(getClaims.Result.Rol) != EnumRoles.Empleado)
            {
                throw new Exception("User Not authorized");
            }

            int restaurantID = repoEmployeeRestaurant.GetByID(int.Parse(getClaims.Result.Id)).RestauranteNIT;            
            List<Pedidos> pedidos = new List<Pedidos>();
            pedidos = repoOrders.GetOrders(restaurantID);
            if (filter.Estado != 0)
            {
                pedidos = pedidos.Where(x=> x.Estado == filter.Estado).ToList();
            }

            paginas = (int)pedidos.Count() / filter.ElementosPorPagina;
            if (pedidos.Count() % filter.ElementosPorPagina != 0)
            {
                paginas += 1;
            }

            for (int i = 1; i<= pedidos.Count(); i++)
            {

            }

            return pedidos;         
        }
    }
}
