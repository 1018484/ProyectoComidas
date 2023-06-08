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
using Dominio.Modelos.DTO;

namespace Applicacion.Repositorio
{
    public class UsuariosServicio : IUsuarioServicio
    {
        private readonly IRepositorioBase<Usuarios, int> repoUsuarios;

        private readonly IRoles repoRoles;

        private readonly IRepositorioRestauranteEmpleados<RestauranteEmpleados, int> repoRestauranteEmpleados;

        private Validaciones validaciones;       

        public UsuariosServicio(IRepositorioBase<Usuarios, int> repoUsuarios, IRoles Roles, IRepositorioRestauranteEmpleados<RestauranteEmpleados, int> repoRestauranteEmpleados)
        {
            this.repoUsuarios = repoUsuarios;
            this.repoRoles = Roles;
            validaciones = new Validaciones();
            this.repoRestauranteEmpleados = repoRestauranteEmpleados;
        } 

        public Usuarios AgregarPropietario(Usuarios entidad)
        {
            entidad.RolesRolId = (int)EnumRoles.Propietario;
            if (string.IsNullOrEmpty(entidad.Celular) || string.IsNullOrEmpty(entidad.Apellido) || string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Clave) || entidad.DocumentoId == null)
            {
                throw new Exception("Campos nulos");
            }

            var rol = repoRoles.RolClaims();
            if (rol.Rol != "1")
            {
                throw new Exception("usuario no tiene acceso para crear un Usuario Propietario");
            }

            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }           
                                
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            entidad.Clave = BCrypt.Net.BCrypt.HashPassword(entidad.Clave);
            var result = repoUsuarios.Agregar(entidad);
            repoUsuarios.Confirmar();
            return result;                   
        }

        public async Task<Usuarios> AgregarEmpleado(Usuarios entidad)
        {
            entidad.RolesRolId = (int)EnumRoles.Empleado;
            if (string.IsNullOrEmpty(entidad.Celular) || string.IsNullOrEmpty(entidad.Apellido) || string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Clave) || entidad.DocumentoId == null)
            {
                throw new Exception("Campos nulos");
            }

            var rol = repoRoles.RolClaims();
            if (rol.Rol != "2")
            {
                throw new Exception("usuario no tiene acceso para crear un Usuario Empleado");
            }

            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }
            
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            entidad.Clave = BCrypt.Net.BCrypt.HashPassword(entidad.Clave);
            var result = repoUsuarios.Agregar(entidad);
            repoUsuarios.Confirmar();            
            RestauranteEmpleados restauranteEmpleados = new RestauranteEmpleados()
            {
                EmpleadoId = entidad.DocumentoId,
                EmpleadorId = int.Parse(rol.Id),
                RestauranteNIT_Id = await repoRestauranteEmpleados.GetrestauranteNIT(int.Parse(rol.Id))
            };

            repoRestauranteEmpleados.Agregar(restauranteEmpleados);
            repoRestauranteEmpleados.Confirmar();
            return entidad;
        }


        public Usuarios AgregarUsuario(Usuarios entidad)
        {
            entidad.RolesRolId = (int)EnumRoles.Cliente;
            if (string.IsNullOrEmpty(entidad.Celular) || string.IsNullOrEmpty(entidad.Apellido) || string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Clave) || entidad.DocumentoId == null)
            {
                throw new Exception("Campos nulos");
            }
            if (entidad == null)
            {
                throw new Exception("El Usuario es Requerido");
            }
            
            if (!validaciones.EmailValidation(entidad.Correo))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validaciones.ValidaTelefono(entidad.Celular))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            entidad.Clave = BCrypt.Net.BCrypt.HashPassword(entidad.Clave);
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
        
        public Usuarios ValidaUsusarioContraseña(UsuarioDTO usuarioDTO)
        {
            var _usuarios = repoUsuarios.ObtenerTodos();
            var _usuario = _usuarios.Where(u => u.Correo == usuarioDTO.Correo).FirstOrDefault();
            if (_usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioDTO.Contraseña, _usuario.Clave))
            {
                return null;
            }           

            return _usuario;
        }

        public int ObtenerRestauranteNIT(int id)
        {
            var EmpleadoRestaurante = repoRestauranteEmpleados.obtener(id);
            return EmpleadoRestaurante.RestauranteNIT_Id;
        }
    }
}
