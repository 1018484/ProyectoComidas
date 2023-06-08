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
    public class RestauranteServicio : IRestaruranteServicio
    {
        private readonly IRepositorioRestaurante<Restaurantes, int> repoRestaurantes;
        private readonly IRepositorioUsuariosRemoto<Usuarios, int> repoUsuuariosRemoto;
        private readonly IRoles repoRoles;



        private Validaciones validaciones;
        public RestauranteServicio(IRepositorioRestaurante<Restaurantes, int> repoRestaurante, IRepositorioUsuariosRemoto<Usuarios, int> repoUsuario, IRoles repoRoles)
        {
            this.repoRestaurantes = repoRestaurante;
            this.repoUsuuariosRemoto = repoUsuario;
            validaciones = new Validaciones();
            this.repoRoles = repoRoles; 
        }
        

        public async Task<Restaurantes> Agregar(RestaurantesDTO entidadDTO)
        {

            if (string.IsNullOrEmpty(entidadDTO.Nombre) || string.IsNullOrEmpty(entidadDTO.Direccion) || string.IsNullOrEmpty(entidadDTO.Telefono) || string.IsNullOrEmpty(entidadDTO.URLLogo))
            {
                throw new Exception("Campos nulos");
            }

            var getClaims = await repoRoles.getToken();
            if (getClaims == null)
            {
                throw new Exception("El usuario no ha iniciado sesion");
            }

            if (int.Parse(getClaims.Rol) != (int)EnumRoles.Administrador)
            {
                throw new Exception("usuario no tiene acceso para crear un restaurante");
            }

            if (entidadDTO == null)
            {
                throw new Exception("El Restaurante es Requerido");
            }

            Restaurantes entidad = new Restaurantes()
            {
                NIT_Id = entidadDTO.NIT_Id,
                Nombre = entidadDTO.Nombre,
                Direccion = entidadDTO.Direccion,
                Telefono = entidadDTO.Telefono,
                URLLogo = entidadDTO.URLLogo,
                DocumentoId = entidadDTO.DocumentoId
            };


            if (!validaciones.ValidaTelefono(entidad.Telefono))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            if (validaciones.ValidaNumerico(entidad.Nombre))
            {
                throw new Exception("Nombre de Restaurante Invalido");
            }

            var usuario = await repoUsuuariosRemoto.UsuarioID(entidad.DocumentoId);
            if (usuario == null)
            {
                throw new Exception("Se esta intentando agregar un restaurante a un Usuario no registrado");
            }

            if (usuario.RolesRolId != (int)EnumRoles.Propietario)
            {
                throw new Exception("Se esta intentando agregar un restaurante a un Usuario que no cuenta con permisos para tener uno");
            }

            var result = repoRestaurantes.Agregar(entidad);
            this.repoRestaurantes.Confirmar();
            return result;
        }

        public Restaurantes ObtenerRestauranteNIt_ID(int IdPropietario)
        {
            return repoRestaurantes.ObtenerById(IdPropietario);            
        }
    }
}
