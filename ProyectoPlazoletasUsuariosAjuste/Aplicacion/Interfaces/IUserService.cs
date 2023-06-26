using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// IUser Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add Owner
        /// </summary>
        /// <param name="entityDTO">User Dto</param>  
        void AddOwner(UsuarioDTO entityDTO);

        /// <summary>
        /// Add Emplyee
        /// </summary>
        /// <param name="entityDTO">User Dto</param> 
        void AddEmployee(UsuarioDTO entityDTO);

        /// <summary>
        /// Add client
        /// </summary>
        /// <param name="entityDTO">User Dto</param> 
        void AddUser(UsuarioDTO entityDTO);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param> 
        /// <returns>User</returns>
        Usuarios GetById(int id);

        /// <summary>
        /// Valid Password
        /// </summary>
        /// <param name="userLoguin">User Login Dto</param> 
        /// <returns>User loggedin </returns>
        Usuarios Password(UserLogin userLoguin);
        
    }
}
