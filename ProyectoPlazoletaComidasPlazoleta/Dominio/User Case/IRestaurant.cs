using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    /// <summary>
    /// IRestaurant User case.
    /// </summary>  
    public interface IRestaurant
    {
        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        public void ValidateRol(Task<UsuarioClaims> claims);

        /// <summary>
        /// Validate user.
        /// </summary>  
        /// <param name="user">user</param>
        public void ValidateUser(Usuarios user);


        /// <summary>
        /// Validate Model.
        /// </summary>  
        /// <param name="rest">user</param>
        public void ValidateModel(Restaurantes rest);
       
    }
}
