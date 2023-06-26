using Dominio.DTO;
using Dominio.Interfaces;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicacion.Interfaces
{
    /// <summary>
    /// IRestaurant Service
    /// </summary>
    public interface IRestarurantService
    {
        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="entityDTO">Restaurant DTO</param>
        /// <returns>Restaurant Creeated</returns>
        Task<Restaurantes> AddRestaurant(RestaurantesDTO entityDTO);

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="ownerID">Owner ID</param>        
        Task AddEmployeeRestaurant(int ownerID);        
        
    }
}
