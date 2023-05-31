using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Modelos;
using Dominio.Repositorios;
using Applicacion.Interfaces;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace infrastructure.Repositorios
{
    public class UsuariosRepository : IRepositorioBase<Usuarios, int>
    {
        private Db_Context db_context;

        public UsuariosRepository(Db_Context db_context) { 
        
            this.db_context = db_context;
        }

        public Usuarios Agregar(Usuarios entidad)
        {
            db_context.Usuarios.Add(entidad);
            return entidad;
        }

        public void Confirmar()
        {
            db_context.SaveChanges();
        }

        public void Editar(Usuarios entidad)
        {
            var seleccionado = db_context.Usuarios.Where(u => u.DocumentoId == entidad.DocumentoId).FirstOrDefault();
            if (seleccionado != null)
            {
                seleccionado.Nombre = entidad.Nombre;
                seleccionado.Apellido = entidad.Apellido;
                seleccionado.Correo = entidad.Correo;
                seleccionado.Celular = entidad.Celular;                
                seleccionado.Correo = entidad.Nombre;
                seleccionado.RolesRolId = entidad.RolesRolId;
                db_context.Usuarios.Update(seleccionado);
            }
        }

        public void Eliminar(int id)
        {
            var seleccionado = db_context.Usuarios.Where(u => u.DocumentoId == id).FirstOrDefault();
            if (seleccionado != null)
            {
                db_context.Usuarios.Remove(seleccionado);
            }
        }

        public Usuarios obtener(int id)
        {
            return db_context.Usuarios.Where(u => u.DocumentoId == id).FirstOrDefault();
        }

        public List<Usuarios> ObtenerTodos()
        {
            return db_context.Usuarios.ToList();
        }
    }
}
