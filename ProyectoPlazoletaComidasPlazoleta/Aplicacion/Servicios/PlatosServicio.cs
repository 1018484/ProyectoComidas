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

        public PlatosServicio(IRepositorioPlatos<Platos, string, int> repoPlatos, IRepositorioRestaurante<Restaurantes, int> repoRestaurantes)
        {
            this.repoPlatos = repoPlatos;  
            this.repoRestaurantes = repoRestaurantes;
        }
        public Platos Agregar(Platos entidad, int IDusuario)
        {
            entidad.Id = 0;
            if (entidad == null)
            {
                throw new Exception("El Platos es Requerido");
            }

            var restauranteinfo = repoRestaurantes.obtener(entidad.RestaurantesNIT_Id);
            if(restauranteinfo == null)
            {
                throw new Exception("El restaurante no existe");
            }
            if(restauranteinfo.DocumentoId != IDusuario)
            {
                throw new Exception("El Usuario no puede insertar plato a otro restaurante");
            }            

            var result = this.repoPlatos.Agregar(entidad);
            this.repoPlatos.Confirmar();
            return result;
        }

        public void Editar(Platos entidad, int IDusuario)
        {            
            if (entidad == null)
            {
                throw new Exception("El Platos es Requerido");
            }

            var restauranteinfo = repoRestaurantes.obtener(entidad.RestaurantesNIT_Id);
            if (restauranteinfo.DocumentoId != IDusuario)
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
        }
    }
}
