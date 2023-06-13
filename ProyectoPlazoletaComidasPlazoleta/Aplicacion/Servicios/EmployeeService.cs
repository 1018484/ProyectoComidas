using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
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

        private readonly IDishesOrdersRepository<PedidosPlatos> repoOrdersDishes;

        private readonly IMapper mapper;

        private Task<UsuarioClaims> getClaims;

        public EmployeeService(IOrdersRepository<Pedidos, string> Orders, IRoles Roles, IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> employeeRestaurant, IDishesOrdersRepository<PedidosPlatos> repoOrdersDishes, IMapper mapper)
        {
            this.repoOrders = Orders;
            this.repoRoles = Roles;
            this.repoEmployeeRestaurant = employeeRestaurant;
            this.getClaims = this.repoRoles.getToken();
            this.repoOrdersDishes = repoOrdersDishes;
            this.mapper = mapper;
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

        public async Task<List<PaginacionPedidos>>ListOrders(PedidsoFiltroDTO filter)
        {
            int paginas = 0;           
            if (Enum.Parse<EnumRoles>(getClaims.Result.Rol) != EnumRoles.Empleado)
            {
                throw new Exception("User Not authorized");
            }

            List<PaginacionPedidos> result = new List<PaginacionPedidos>();
            int restaurantID = repoEmployeeRestaurant.GetByID(int.Parse(getClaims.Result.Id)).RestauranteNIT;          
            var ordersDishes = repoOrdersDishes.GetOrders(restaurantID, filter.Estado).GroupBy(p=> p.Pedido_Id);            
            foreach (var orderDish in ordersDishes)
            {
                PaginacionPedidos pagOrders = new PaginacionPedidos();
                pagOrders.PedidoID = orderDish.Key;
                pagOrders.PedidosPlatosDTO = new List<PedidosPlatosDTO>();
                IEnumerable<IGrouping<Guid, PedidosPlatos>> groups = orderDish.GroupBy(x => x.Pedido_Id);
                IEnumerable<PedidosPlatos> dishOrders = groups.SelectMany(group => group);
                List<PedidosPlatos> listDish = new List<PedidosPlatos>();
                listDish = dishOrders.ToList();                
                for (int b= 0; b<=listDish.Count -1; b++ ) 
                {
                    PedidosPlatosDTO pedidosPlatosDTO = new PedidosPlatosDTO(); 
                    pedidosPlatosDTO.Cantidad = listDish[b].Cantidad;
                    pedidosPlatosDTO.Id = listDish[b].Id;
                    pedidosPlatosDTO.Pedidos = new PedidosDTO();
                    pedidosPlatosDTO.Pedidos = mapper.Map<PedidosDTO>(listDish[b].Pedidos);  
                    pedidosPlatosDTO.Platos = new PlatosDTO();
                    pedidosPlatosDTO.Platos = mapper.Map<PlatosDTO>(listDish[b].Platos);
                    pagOrders.PedidosPlatosDTO.Add(pedidosPlatosDTO);
                }

                result.Add(pagOrders);
            }


            return result;


        }
    }
}
