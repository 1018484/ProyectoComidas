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
    public class RestauranteServicio : IRestaruranteServicio<RestaurantesDTO>
    {
        private readonly IRepositorioRestaurante<Restaurantes, int> repoRestaurantes;
        private readonly IRepositorioUsuariosRemoto<Usuarios, int> repoUsuuariosRemoto;
       

        private Validaciones validaciones;
        public RestauranteServicio(IRepositorioRestaurante<Restaurantes, int> repoRestaurante, IRepositorioUsuariosRemoto<Usuarios, int> repoUsuario)
        {
            this.repoRestaurantes = repoRestaurante;
            this.repoUsuuariosRemoto = repoUsuario;
            validaciones = new Validaciones();
        }
        public async Task Agregar(RestaurantesDTO entidadDTO)
        {
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

            //CONSUMIR Usuarios

            var usuario =  await repoUsuuariosRemoto.UsuarioID(entidad.DocumentoId);
            if (usuario == null)
            {
                throw new Exception("Se esta intentando agregar un restaurante a un Usuario no registrado");
            }

            if (usuario.RolesRolId != (int)EnumRoles.Propietario)
            {
                throw new Exception("Se esta intentando agregar un restaurante a un Usuario que no cuenta con permisos para tener uno");
            }

            var result = this.repoRestaurantes.Agregar(entidad);
            this.repoRestaurantes.Confirmar();
           
        }
    }
}
