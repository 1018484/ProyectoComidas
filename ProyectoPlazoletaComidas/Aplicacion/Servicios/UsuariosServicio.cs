using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Repositorios;
using System.Text.RegularExpressions;
using Aplicacion.Validaciones;

namespace Applicacion.Repositorio
{
    public class UsuariosServicio : IUsuarioServicio
    {
        private readonly IRepositorioBase<Usuarios, int> repoUsuarios;

        private Validaciones validaciones;

        public UsuariosServicio(IRepositorioBase<Usuarios, int> repoUsuarios)
        {
            this.repoUsuarios = repoUsuarios;
            validaciones = new Validaciones();
        } 

        public Usuarios AgregarPropietario(Usuarios entidad)
        {
            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }
           
            entidad.RolesRolId = (int)EnumRoles.Propietario;                      
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            var result = repoUsuarios.Agregar(entidad);
            repoUsuarios.Confirmar();
            return result;                   
        }

        public Usuarios AgregarEmpleado(Usuarios entidad)
        {
            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }

            entidad.RolesRolId = (int)EnumRoles.Empleado;
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            var result = repoUsuarios.Agregar(entidad);
            repoUsuarios.Confirmar();
            return result;
        }


        public Usuarios AgregarUsuario(Usuarios entidad)
        {
            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }

            entidad.RolesRolId = (int)EnumRoles.Cliente;
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            var result = repoUsuarios.Agregar(entidad);
            repoUsuarios.Confirmar();
            return result;
        }

        public Usuarios obtener(int id)
        {
            return repoUsuarios.obtener(id);
        }

        public List<Usuarios> ObtenerTodos()
        {
            return repoUsuarios.ObtenerTodos();
        }       
    }
}
