using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public class Client : IClients
    {
        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        public void ValidateRol(Task<UsuarioClaims> claims)
        {
            if (Enum.Parse<EnumRoles>(claims.Result.Rol) != EnumRoles.Cliente)
            {
                throw new Exception("User Not authorized"); ;
            }
        }

        /// <summary>
        /// validate pending orders
        /// </summary>  
        /// <param name="orders">Order</param>
        public void ValidOrders(Pedidos orders)
        {
            if (orders != null)
            {
                throw new Exception("The User cannot create another order until their queued orders are finished");
            }
        }


        /// <summary>
        /// dishes paging
        /// </summary>  
        /// <param name="RestaurantGroup">grouped list of dishes </param>
        /// <returns>dishes paging</returns>
        public List<PaginacionPlatosDTO> ListDishes(int pag, IEnumerable<IGrouping<int, Platos>> RestaurantGroup)
        {
            int page = 0;
            List<PaginacionPlatosDTO> result = new List<PaginacionPlatosDTO>();
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
                    IEnumerable<IGrouping<int, Platos>> groups = dish.GroupBy(x => x.RestaurantesNIT_Id);
                    IEnumerable<Platos> platos = groups.SelectMany(group => group);
                    List<Platos> listDish = new List<Platos>();
                    listDish = platos.ToList();
                    page = (int)listDish.Count() / pag;
                    if (listDish.Count() % pag != 0)
                    {
                        page += 1;
                    }

                    for (int i = 1; i <= page; i++)
                    {
                        PaginacionPlatos pagiDishes = new PaginacionPlatos();
                        pagiDishes.DatosPorPagina = pag;
                        pagiDishes.CantidadDePaginas = page;
                        pagiDishes.NumeroDePagina = i;
                        pagiDishes.Filtrados = new List<Platos>();
                        for (int a = 1; a <= pag; a++)
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

        /// <summary>
        /// restaurants paging
        /// </summary>  
        /// <param name="restaurant">grouped list of restaurant </param>
        /// <returns>restaurant paging</returns>
        public List<PaginacionRestaurantesDTO> ListRestaurants(int pag, List<RestaurantesfiltradosDTO> restaurant)
        {
            int page = 0;
            List<PaginacionRestaurantesDTO> paginacionlist = new List<PaginacionRestaurantesDTO>();
            page = (int)restaurant.Count() / pag;
            if (restaurant.Count() % pag != 0)
            {
                page += 1;
            }

            for (int i = 1; i <= page; i++)
            {
                PaginacionRestaurantesDTO pagByPagination = new PaginacionRestaurantesDTO();
                pagByPagination.DatosPorPagina = pag;
                pagByPagination.CantidadDePaginas = page;
                pagByPagination.NumeroDePagina = i;
                pagByPagination.Filtrados = new List<RestaurantesfiltradosDTO>();
                for (int a = 1; a <= pag; a++)
                {
                    if (restaurant.Count == 0)
                    {
                        break;
                    }

                    pagByPagination.Filtrados.Add(restaurant[0]);
                    restaurant.Remove(restaurant[0]);
                }

                paginacionlist.Add(pagByPagination);
            }

            return paginacionlist;
        }       
    }
}
