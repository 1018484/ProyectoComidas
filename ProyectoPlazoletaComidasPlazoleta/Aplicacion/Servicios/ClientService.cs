using Aplicacion.Interfaces;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Dominio.User_Case;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ClientService : IClientService
    {
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;

        private readonly IDishesRepository<Platos, string, int> repoPDishes ;

        private readonly IOrdersRepository<Pedidos, string> repoOrders ;

        private readonly IRoles repoRoles;

        private readonly IDishesOrdersRepository<PedidosPlatos> repoOrdersDishes;

        private readonly IClients useClients;

        private Task<UsuarioClaims> getClaims;


        public ClientService(IRestaurantRespository<Restaurantes, int> rest, IDishesRepository<Platos, string, int> dish, IOrdersRepository<Pedidos, string> orders, IRoles roles, IDishesOrdersRepository<PedidosPlatos> orderDish, IClients useClients)
        {
            this.repoRestaurant = rest;
            this.repoPDishes = dish;
            this.repoOrders = orders;
            this.repoRoles = roles;
            this.repoOrdersDishes = orderDish;
            this.getClaims = this.repoRoles.getToken();
            this.useClients = useClients;
        }
        /// <summary>
        /// This property always returns a value &lt; 1.
        /// </summary>
        /// <param name="SendOrder">description</param>
        public async Task AddOrders(SendOrder entityDTO)
        {
            useClients.ValidateRol(getClaims);
            var orders = this.repoOrders.GetByID(getClaims.Result.Id);
            useClients.ValidOrders(orders);
            Pedidos order = new Pedidos();
            order.Pedido_Id = Guid.NewGuid();
            order.Cliente_Id = getClaims.Result.Id;
            order.Fecha = DateTime.Now;            
            order.Estado = (int)EnumStatus.Pendiente;
            order.RestaurantesNIT_Id = entityDTO.RestauranteNIT;
            this.repoOrders.Add(order);
            this.repoOrders.Confirm();
            AgendarPlatos(order.Pedido_Id, entityDTO.platos);
        }

        public List<PaginacionPlatosDTO> ListDishes(int pag)
        {
            int page = 0;
            List<PaginacionPlatosDTO> result = new List<PaginacionPlatosDTO>();            
            var RestaurantGroup = repoPDishes.GetAll().GroupBy(x => x.RestaurantesNIT_Id);
            return useClients.ListDishes(pag, RestaurantGroup);
           
        }

        public List<PaginacionRestaurantesDTO> ListRestaurants(int pag)
        {            
            var restaurant = repoRestaurant.GetAll().Select(x=> new RestaurantesfiltradosDTO()
            {
                Nombre = x.Nombre,
                URLLogo = x.URLLogo
            }).ToList();

            return useClients.ListRestaurants(pag, restaurant);
           
        }
        
        public void AgendarPlatos(Guid id, List<PlatosPedidosDTO> dishes)
        {
            foreach (var dish in dishes)
            {
                PedidosPlatos pedidosPlatos = new PedidosPlatos()
                {
                    Pedido_Id = id,
                    Id = dish.IdPlato,
                    Cantidad=dish.Cantidad 
                };

                repoOrdersDishes.Add(pedidosPlatos);
                repoOrdersDishes.Confirm();
            }
        }
    }
}
