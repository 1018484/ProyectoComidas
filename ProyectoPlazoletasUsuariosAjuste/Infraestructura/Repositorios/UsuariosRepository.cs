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
    public class UsuariosRepository : IUserRespository
    {
        private Db_Context db_context;

        public UsuariosRepository(Db_Context db_context)
        {

            this.db_context = db_context;
        }

        public Usuarios Add(Usuarios entity)
        {
            entity.Clave = BCrypt.Net.BCrypt.HashPassword(entity.Clave);
            db_context.Usuarios.Add(entity);
            return entity;
        }

        public void Confirm()
        {
            db_context.SaveChanges();
        }

        public void Edit(Usuarios entity)
        {
            var seleccionado = db_context.Usuarios.Where(u => u.DocumentoId == entity.DocumentoId).FirstOrDefault();
            if (seleccionado != null)
            {
                seleccionado.Nombre = entity.Nombre;
                seleccionado.Apellido = entity.Apellido;
                seleccionado.Correo = entity.Correo;
                seleccionado.Celular = entity.Celular;
                seleccionado.Correo = entity.Nombre;
                seleccionado.RolesRolId = entity.RolesRolId;
                db_context.Usuarios.Update(seleccionado);
            }
        }

        public void Delete(int id)
        {
            var seleccionado = db_context.Usuarios.Where(u => u.DocumentoId == id).FirstOrDefault();
            if (seleccionado != null)
            {
                db_context.Usuarios.Remove(seleccionado);
            }
        }

        public Usuarios GetByID(int id)
        {
            return db_context.Usuarios.Where(u => u.DocumentoId == id).FirstOrDefault();
        }

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
