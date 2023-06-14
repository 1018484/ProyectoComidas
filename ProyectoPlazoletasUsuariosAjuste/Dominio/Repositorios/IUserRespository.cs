using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    /// <summary>
    /// User Repository DbSets
    /// a los alumnos.
    /// </summary>  
    public interface IUserRespository
    {
        /// <summary>
        /// Add Users
        /// </summary>
        /// <param name="entity">User model</param>
        /// <returns>User Add</returns>
        Usuarios Add(Usuarios entity);

        /// <summary>
        /// Save Chages
        /// </summary> 
        void Confirm();

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        Usuarios GetByID(int id);

        /// <summary>
        /// Valid user and Password
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>User logged in </returns>
        Usuarios ValidPassword(UserLogin login);
    }
}
