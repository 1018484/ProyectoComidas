using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Repositorio
{
    public class DishesService : IDishesService
    {
        private readonly IDishesRepository<Platos, string, int> repoDishes;
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;
        private readonly IRoles repoRoles;
        private readonly IMapper mapper;
        private Task<UsuarioClaims> getClaims;

        public DishesService(IDishesRepository<Platos, string, int> repoPlatos, IRestaurantRespository<Restaurantes, int> repoRestaurantes, IRoles roles, IMapper mapper)
        {
            this.repoDishes = repoPlatos;  
            this.repoRestaurant = repoRestaurantes;
            this.repoRoles = roles;
            this.mapper = mapper;
            this.getClaims = this.repoRoles.getToken();
        }
        public async Task<Platos> AddDish(PlatosDTO entiyDTO)
        {
            Platos dish = mapper.Map<Platos>(entiyDTO);           
            if (int.Parse(getClaims.Result.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("User Not authorized");
            }

            dish.Id = 0;
            dish.Activo = true;      
            var restauranteinfo = repoRestaurant.GetByID(dish.RestaurantesNIT_Id);
            if(restauranteinfo == null)
            {
                throw new Exception("The restaurant does not exist");
            }

            if(restauranteinfo.DocumentoId != int.Parse(getClaims.Result.Id))
            {
                throw new Exception("The User cannot insert a dish to another restaurant");
            }            

            var result = this.repoDishes.Add(dish);
            this.repoDishes.Confirm();
            return result;
        }

        public async Task<Platos> EditDish(PlatosDTO entidadDTO)
        {
            Platos entidad = mapper.Map<Platos>(entidadDTO);                          
            if (int.Parse(getClaims.Result.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("User Not authorized");
            }           

            var restauranteinfo = repoRestaurant.GetByID(entidad.RestaurantesNIT_Id);
            if (restauranteinfo.DocumentoId != int.Parse(getClaims.Result.Id))
            {
                throw new Exception("The User cannot Edit a dish to another restaurant");
            }

            var seleccionado = repoDishes.GetByRestaurantNIT(entidad.NombrePlato, entidad.RestaurantesNIT_Id);
            if (seleccionado == null)
            {
                throw new Exception("El Plato a editar no existe");
            }

            seleccionado.Precio = entidad.Precio;
            seleccionado.Desacripcion = entidad.Desacripcion;
            seleccionado.Activo = entidad.Activo;
            repoDishes.Edit(seleccionado);
            repoDishes.Confirm();
            return seleccionado;
        }
    }
}
