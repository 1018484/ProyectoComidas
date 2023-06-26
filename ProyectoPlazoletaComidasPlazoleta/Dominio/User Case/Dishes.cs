using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public class Dishes : IDishes
    {

        /// <summary>
        /// validates that the dish exists
        /// </summary>  
        /// <param name="dish">Dish</param>    
        public void ValidateDish(Platos dish)
        {
            if (dish == null)
            {
                throw new Exception("El Plato a editar no existe");
            }
        }

        /// <summary>
        /// Valid restaurant owner
        /// </summary>  
        /// <param name="claims">User logged in</param>
        /// <param name="res">Restaurant</param>
        public void ValidateRestaurant(Restaurantes res, Task<UsuarioClaims> claims)
        {
            if (res == null)
            {
                throw new Exception("The restaurant does not exist");
            }

            if (res.DocumentoId != int.Parse(claims.Result.Id))
            {
                throw new Exception("The User cannot insert a dish to another restaurant");
            }
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

            if (int.Parse(claims.Result.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("User Not authorized");
            }
        }
    }
}
