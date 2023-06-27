using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Dominio.User_Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    /// <summary>
    /// Employee Service Class.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Repository Orders DBbSet
        /// </summary>
        private readonly IOrdersRepository<Pedidos, string> repoOrders;

        /// <summary>
        /// Repository EmployeeRestaurant DBbSet
        /// </summary>
        private readonly IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> repoEmployeeRestaurant;

        /// <summary>
        /// Repository Valid token and sesion
        /// </summary>
        private readonly IRoles repoRoles;

        /// <summary>
        /// Repository DishOrders DBbSet
        /// </summary>
        private readonly IDishesOrdersRepository<PedidosPlatos, Guid> repoOrdersDishes;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Repository Message remoto httpclient
        /// </summary>
        private readonly IMessageRemotoRepository repoMessage;

        /// <summary>
        /// Repository employee DBbSet
        /// </summary>
        private readonly IEmployee useEmployee;

        /// <summary>
        /// Repository Get User Remoto httpCLient
        /// </summary>
        private readonly IUsersRemotoRepository<Usuarios, int> repoUerRemoto;      


        /// <summary>
        /// User sesion
        /// </summary>
        /// <param name="employeeRestaurant">Intance Repository EmployeeRestaurant</param>
        /// <param name="mapper">Intance Autommapper</param>
        /// <param name="Orders">Intance Repository orders</param>
        /// <param name="repoMessage">Intance Repository Message</param>
        /// <param name="repoOrdersDishes">Intance Repository OrdersDishes</param>
        /// <param name="repoUerRemoto">Intance Repository userremoto</param>
        /// <param name="Roles">Intance Repository Roles</param>
        /// <param name="useEmployee">Intance use case Employee</param>
        public EmployeeService(IOrdersRepository<Pedidos, string> Orders, IRoles Roles, IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> employeeRestaurant, IDishesOrdersRepository<PedidosPlatos, Guid> repoOrdersDishes, IMapper mapper, IEmployee useEmployee, IMessageRemotoRepository repoMessage, IUsersRemotoRepository<Usuarios, int> repoUerRemoto)
        {
            this.repoOrders = Orders;
            this.repoRoles = Roles;
            this.repoEmployeeRestaurant = employeeRestaurant;          
            this.repoOrdersDishes = repoOrdersDishes;
            this.mapper = mapper; 
            this.useEmployee = useEmployee;
            this.repoMessage = repoMessage;
            this.repoUerRemoto = repoUerRemoto;
        }

        /// <summary>
        /// Assing orders to employee
        /// </summary>
        /// <param name="orders">List orders to assign</param>
        public void AssignOrder(List<Guid> orders)
        { 
            throw new NotImplementedException();                   
        }

        /// <summary>
        /// to List Orders by restaurant and Status
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>List orders</returns>
        public async Task<List<PaginacionPedidos>>ListOrders(PedidsoFiltroDTO filter)
        {
            throw new NotImplementedException();            
        }

        /// <summary>
        /// change order status 
        /// </summary>
        /// <param name="dto">Status</param>
        public async Task StatusAsync(CambiarEstados dto)
        {
            Random rnd = new Random();
            //useEmployee.ValidateRol(getClaims);
            var order = repoOrders.GetOrder(dto.PedidoId);
            var user = await repoUerRemoto.GetUserID(int.Parse(order.Cliente_Id));

            if (dto.Estado == (int)EnumStatus.Listo)
            {
                TWMessage message = new TWMessage()
                {
                    From = "+13614016214",
                    To = user.Celular,
                    Message = rnd.Next(0, 1000).ToString(),
                };
                
                //await repoMessage.SendMessageAsync(message);
            }
            else if (dto.Estado == (int)EnumStatus.Entregado)
            {
                if (order.Estado != (int)EnumStatus.Listo)
                {
                    throw new Exception("Invalid Status Changee");
                }

                if(order.Codigo != dto.Codigo)
                {
                    throw new Exception("Invalid Code");
                }
                
            }

        }
    }
}
