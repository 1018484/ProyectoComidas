using Aplicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Repositorio
{
    public class PlatosServicio : IPlatosServicio<Platos,int>
    {
        private readonly IRepositorioPlatos<Platos, string, int> repoPlatos;     
        private readonly IRepositorioRestaurante<Restaurantes, int> repoRestaurantes;
        private readonly IRoles repoRoles;

        public PlatosServicio(IRepositorioPlatos<Platos, string, int> repoPlatos, IRepositorioRestaurante<Restaurantes, int> repoRestaurantes, IRoles roles)
        {
            this.repoPlatos = repoPlatos;  
            this.repoRestaurantes = repoRestaurantes;
            this.repoRoles = roles;
        }
        public async Task<Platos> Agregar(Platos entidad, int IDusuario)
        {
            var getClaims = await repoRoles.getToken();
            if (getClaims == null)
            {
                throw new Exception("El usuario no ha iniciado sesion");
            }

            if (int.Parse(getClaims.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("usuario no tiene acceso para crear un Plato");
            }

            entidad.Id = 0;
            entidad.Activo = true;
            if (entidad == null)
            {
                throw new Exception("El Platos es Requerido");
            }

            var restauranteinfo = repoRestaurantes.obtener(entidad.RestaurantesNIT_Id);
            if(restauranteinfo == null)
            {
                throw new Exception("El restaurante no existe");
            }

            if(restauranteinfo.DocumentoId != int.Parse(getClaims.Id))
            {
                throw new Exception("El Usuario no puede insertar plato a otro restaurante");
            }            

            var result = this.repoPlatos.Agregar(entidad);
            this.repoPlatos.Confirmar();
            return result;
        }

        public async Task<Platos> EditarAsync(Platos entidad, int IDusuario)
        {
            var getClaims = await repoRoles.getToken(); ;
            if (getClaims == null)
            {
                throw new Exception("El usuario no ha iniciado sesion");
            }

            if (int.Parse(getClaims.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("usuario no tiene acceso para Editar un Plato");
            }
            if (entidad == null)
            {
                throw new Exception("El Platos es Requerido");
            }

            var restauranteinfo = repoRestaurantes.obtener(entidad.RestaurantesNIT_Id);
            if (restauranteinfo.DocumentoId != int.Parse(getClaims.Id))
            {
                throw new Exception("El Usuario no puede Editar plato a otro restaurante");
            }

            var seleccionado = repoPlatos.ConsultarPlatoPorRestaurante(entidad.NombrePlato, entidad.RestaurantesNIT_Id);
            if (seleccionado == null)
            {
                throw new Exception("El Plato a editar no existe");
            }

            seleccionado.Precio = entidad.Precio;
            seleccionado.Desacripcion = entidad.Desacripcion;
            seleccionado.Activo = entidad.Activo;
            repoPlatos.Editar(seleccionado);
            repoPlatos.Confirmar();
            return seleccionado;
        }
    }
}
