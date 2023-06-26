using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public class Employee : IEmployee
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// initialize class.
        /// </summary>   
        /// <param name="mapper">AutoMapper</param>
        public Employee(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        public void ValidateRol(Task<UsuarioClaims> claims)
        {
            if (claims == null)
            {
                throw new Exception("Session expired or session not started");
            }

            if (Enum.Parse<EnumRoles>(claims.Result.Rol) != EnumRoles.Empleado)
            {
                throw new Exception("User Not authorized");
            }
        }

        /// <summary>
        /// order paging
        /// </summary>  
        /// <param name="ordersDishes">User logged in</param>
        /// <returns>List order paging</returns>
        public List<PaginacionPedidos> pageOders(IEnumerable<IGrouping<Guid, PedidosPlatos>> ordersDishes)
        {
            List<PaginacionPedidos> result = new List<PaginacionPedidos>();
            foreach (var orderDish in ordersDishes)
            {
                PaginacionPedidos pagOrders = new PaginacionPedidos();
                pagOrders.PedidoID = orderDish.Key;
                pagOrders.PedidosPlatosDTO = new List<PedidosPlatosDTO>();
                IEnumerable<IGrouping<Guid, PedidosPlatos>> groups = orderDish.GroupBy(x => x.Pedido_Id);
                IEnumerable<PedidosPlatos> dishOrders = groups.SelectMany(group => group);
                List<PedidosPlatos> listDish = new List<PedidosPlatos>();
                listDish = dishOrders.ToList();
                for (int b = 0; b <= listDish.Count - 1; b++)
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
