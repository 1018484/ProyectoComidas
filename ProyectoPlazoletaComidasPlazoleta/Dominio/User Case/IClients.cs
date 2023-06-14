using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public interface IClients
    {
        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        void ValidateRol(Task<UsuarioClaims> claims);

        /// <summary>
        /// validate pending orders
        /// </summary>  
        /// <param name="orders">Order</param>
        void ValidOrders(Pedidos orders);

        /// <summary>
        /// dishes paging
        /// </summary>  
        /// <param name="RestaurantGroup">grouped list of dishes </param>
        /// <returns>dishes paging</returns>
        List<PaginacionPlatosDTO> ListDishes(int pag, IEnumerable<IGrouping<int, Platos>> RestaurantGroup);

        /// <summary>
        /// restaurants paging
        /// </summary>  
        /// <param name="restaurant">grouped list of restaurant </param>
        /// <returns>restaurant paging</returns>
        List<PaginacionRestaurantesDTO> ListRestaurants(int pag, List<RestaurantesfiltradosDTO> restaurant);


    }
}
