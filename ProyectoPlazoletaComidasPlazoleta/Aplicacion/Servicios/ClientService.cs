using Aplicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
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

        private Task<UsuarioClaims> getClaims;


        public ClientService(IRestaurantRespository<Restaurantes, int> rest, IDishesRepository<Platos, string, int> dish, IOrdersRepository<Pedidos, string> orders, IRoles roles, IDishesOrdersRepository<PedidosPlatos> orderDish)
        {
            this.repoRestaurant = rest;
            this.repoPDishes = dish;
            this.repoOrders = orders;
            this.repoRoles = roles;
            this.repoOrdersDishes = orderDish;
            this.getClaims = this.repoRoles.getToken();
        }

        public async Task AddOrders(PedidosDTO entityDTO)
        {            
            if (Enum.Parse<EnumRoles>(getClaims.Result.Rol)  != EnumRoles.Cliente)
            {
                throw new Exception("User Not authorized"); ;
            }
            
            if (this.repoOrders.GetByID(getClaims.Result.Id) != null)
            {
                throw new Exception("The User cannot create another order until their queued orders are finished");
            }

            Pedidos order = new Pedidos();
            order.Pedido_Id = Guid.NewGuid();
            order.Cliente_Id = getClaims.Result.Id;
            order.Fecha = DateTime.Now;            
            order.Estado = (int)EnumEstados.Pendiente;
            order.RestaurantesNIT_Id = entityDTO.RestauranteNIT;
            this.repoOrders.Add(order);
            this.repoOrders.Confirm();
            AgendarPlatos(order.Pedido_Id, entityDTO.platos);
        }

        public List<PaginacionPlatosDTO> ListDishes(int paginacion)
        {
            int page = 0;
            List<PaginacionPlatosDTO> result = new List<PaginacionPlatosDTO>();            
            var RestaurantGroup = repoPDishes.GetAll().GroupBy(x => x.RestaurantesNIT_Id);                 
            foreach (var restaurant in RestaurantGroup)
            {
                PaginacionPlatosDTO dto = new PaginacionPlatosDTO();
                dto.CategoriasPlatos = new List<CategoriasPlatos>();
                dto.NitRestaurante = restaurant.Key;
                var dishGroup = restaurant.GroupBy(x => x.Categoria);               
                foreach (var dish in dishGroup)                
                {   
                    CategoriasPlatos category = new CategoriasPlatos();
                    category.PaginacionPlatos = new List<PaginacionPlatos>();
                    category.Categoria = dish.Key;
                    IEnumerable<IGrouping<int, Platos>> groups = dish.GroupBy(x=> x.RestaurantesNIT_Id);
                    IEnumerable<Platos> platos = groups.SelectMany(group => group);
                    List<Platos> listDish = new List<Platos>();
                    listDish = platos.ToList();
                    page = (int)listDish.Count() / paginacion;
                    if (listDish.Count() % paginacion != 0)
                    {
                        page += 1;
                    }

                    for (int i = 1; i <= page; i++)
                    {
                        PaginacionPlatos pagiDishes = new PaginacionPlatos();                        
                        pagiDishes.DatosPorPagina = paginacion;
                        pagiDishes.CantidadDePaginas = page;
                        pagiDishes.NumeroDePagina = i;
                        pagiDishes.Filtrados = new List<Platos>();
                        for (int a = 1; a <= paginacion; a++)
                        {
                            if (listDish.Count == 0)
                            {
                                break;
                            }

                            pagiDishes.Filtrados.Add(listDish[0]);
                            listDish.Remove(listDish[0]);
                        }

                        category.PaginacionPlatos.Add(pagiDishes);
                    }

                    dto.CategoriasPlatos.Add(category);
                }

                result.Add(dto);
            }            
            
            return result;               
        }

        public List<PaginacionRestaurantesDTO> ListRestaurants(int paginacion)
        {
            int paginas = 0;
            List<PaginacionRestaurantesDTO> paginacionlist = new List<PaginacionRestaurantesDTO> ();
            var restaurantes = repoRestaurant.GetAll().Select(x=> new RestaurantesfiltradosDTO()
            {
                Nombre = x.Nombre,
                URLLogo = x.URLLogo
            }).ToList();

            paginas = (int)restaurantes.Count() / paginacion;
            if (restaurantes.Count() % paginacion != 0 )
            {
                paginas += 1;
            }

            for (int i =1; i<= paginas; i++)
            {
                PaginacionRestaurantesDTO pretaurantePorPagina = new PaginacionRestaurantesDTO();
                pretaurantePorPagina.DatosPorPagina = paginacion;
                pretaurantePorPagina.CantidadDePaginas = paginas;
                pretaurantePorPagina.NumeroDePagina = i;
                pretaurantePorPagina.Filtrados = new List<RestaurantesfiltradosDTO>();
                for (int a = 1; a<= paginacion; a++)
                {
                    if (restaurantes.Count == 0)
                    {
                        break;
                    }

                    pretaurantePorPagina.Filtrados.Add(restaurantes[0]);
                    restaurantes.Remove(restaurantes[0]);
                }

                paginacionlist.Add(pretaurantePorPagina);
            }

            return paginacionlist;
        }
        
        public void AgendarPlatos(Guid id, List<PlatosPedidosDTO> platos)
        {
            foreach (var plato in platos)
            {
                PedidosPlatos pedidosPlatos = new PedidosPlatos()
                {
                    Pedido_Id = id,
                    Id = plato.IdPlato,
                    Cantidad=plato.Cantidad 
                };

                repoOrdersDishes.Add(pedidosPlatos);
                repoOrdersDishes.Confirm();
            }
        }
    }
}
