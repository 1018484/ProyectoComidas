using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Infraestructura.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    /// <summary>
    /// User Repository DbSets   
    /// </summary>   
    public class UsuariosRepository : IUserRespository
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context db_context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="db_context">DbContext.</param>
        public UsuariosRepository(Db_Context db_context)
        {

            this.db_context = db_context;
        }

        /// <summary>
        /// Add Users
        /// </summary>
        /// <param name="entity">User model</param>
        /// <returns>User Add</returns>
        public Usuarios Add(Usuarios entity)
        {
            entity.Clave = BCrypt.Net.BCrypt.HashPassword(entity.Clave);
            db_context.Usuarios.Add(entity);
            return entity;
        }

        /// <summary>
        /// Save Chages
        /// </summary> 
        public void Confirm()
        {
            db_context.SaveChanges();
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        public Usuarios GetByID(int id)
        {
            return db_context.Usuarios.Where(u => u.DocumentoId == id).FirstOrDefault();
        }

        /// <summary>
        /// Valid user and Password
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>User logged in </returns>
        public Usuarios ValidPassword(UserLogin login)
        {           
            var users = db_context.Usuarios.ToList();
            var user = users.Where(u => u.Correo == login.Email).FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Clave))
            {
                return null;
            }

            return user;
        }
    }
}
