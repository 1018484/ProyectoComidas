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
        /// <summary>
        /// Repository Restaurant DbSet
        /// </summary>
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;

        /// <summary>
        /// Dishes Restaurant DbSet
        /// </summary>
        private readonly IDishesRepository<Platos, string, int> repoPDishes;

        /// <summary>
        /// Repository Orders DBbSet
        /// </summary>
        private readonly IOrdersRepository<Pedidos, string> repoOrders ;

        /// <summary>
        /// Repository Valid token and sesion
        /// </summary>
        private readonly IRoles repoRoles;

        /// <summary>
        /// Repository DishOrders DBbSet
        /// </summary>
        private readonly IDishesOrdersRepository<PedidosPlatos, Guid> repoOrdersDishes;

        /// <summary>
        /// Use Case Clients
        /// </summary>
        private readonly IClients useClients;        

        /// <summary>
        /// User sesion
        /// </summary
        public ClientService(IRestaurantRespository<Restaurantes, int> rest, IDishesRepository<Platos, string, int> dish, IOrdersRepository<Pedidos, string> orders, IRoles roles, IDishesOrdersRepository<PedidosPlatos, Guid> orderDish, IClients useClients)
        {
            this.repoRestaurant = rest;
            this.repoPDishes = dish;
            this.repoOrders = orders;
            this.repoRoles = roles;
            this.repoOrdersDishes = orderDish;            
            this.useClients = useClients;
        }

        /// <summary>
        /// Add Order.
        /// </summary>
        /// <param name="entityDTO">description</param>
        public async Task AddOrders(SendOrder entityDTO)
        {            
            Pedidos order = new Pedidos();
            order.Pedido_Id = Guid.NewGuid();            
            order.Fecha = DateTime.Now;            
            order.Estado = (int)EnumStatus.Pendiente;
            order.RestaurantesNIT_Id = entityDTO.RestauranteNIT;
            this.repoOrders.Add(order);
            this.repoOrders.Confirm();
            AddDishesDescription(order.Pedido_Id, entityDTO.platos);
        }

        /// <summary>
        /// List to Dishes by restaurant
        /// </summary>
        /// <param name="pag">data for page</param>
        /// <returns>List Dishes </returns>
        public List<PaginacionPlatosDTO> ListDishes(int pag)
        {          
            var RestaurantGroup = repoPDishes.GetAll().GroupBy(x => x.RestaurantesNIT_Id);
            return useClients.ListDishes(pag, RestaurantGroup);
           
        }

        /// <summary>
        /// List to restaurants
        /// </summary>
        /// <param name="pag">data for page</param>
        /// <returns>List restaurants </returns>
        public List<PaginacionRestaurantesDTO> ListRestaurants(int pag)
        {           
            var restaurant = repoRestaurant.GetAll().Select(x=> new RestaurantesfiltradosDTO()
            {
                Nombre = x.Nombre,
                URLLogo = x.URLLogo
            }).ToList();

            return useClients.ListRestaurants(pag, restaurant);
           
        }

        /// <summary>
        /// Add dishes description
        /// </summary>
        /// <param name="id">data for page</param>
        /// <param name="dishes">List dishes</param>        
        public void AddDishesDescription(Guid id, List<PlatosPedidosDTO> dishes)
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

        public void CancelOrder(Guid orderID)
        {            
            repoOrdersDishes.Delete(orderID);
            repoOrdersDishes.Confirm();

        }
    }
}
