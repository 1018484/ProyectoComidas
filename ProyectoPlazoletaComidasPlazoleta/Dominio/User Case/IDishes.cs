using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public  interface IDishes
    {
        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        public void ValidateRol(Task<UsuarioClaims> claims);

        /// <summary>
        /// Valid restaurant owner
        /// </summary>  
        /// <param name="claims">User logged in</param>
        /// <param name="res">Restaurant</param>
        public void ValidateRestaurant(Restaurantes res, Task<UsuarioClaims> claims);

        /// <summary>
        /// validates that the dish exists
        /// </summary>  
        /// <param name="dish">Dish</param>      
        public void ValidateDish(Platos dish);


    }
}
