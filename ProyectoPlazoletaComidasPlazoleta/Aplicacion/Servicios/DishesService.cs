using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Dominio.User_Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Repositorio
{
    public class DishesService : IDishesService
    {
        /// <summary>
        /// Dishes Restaurant DbSet
        /// </summary>
        private readonly IDishesRepository<Platos, string, int> repoDishes;

        /// <summary>
        /// Repository Restaurant DbSet
        /// </summary>
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;

        /// <summary>
        /// Repository Valid token and sesion
        /// </summary>
        private readonly IRoles repoRoles;

        // <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Repository employee DBbSet
        /// </summary>
        private readonly IDishes useDishes;

        /// <summary>
        /// User sesion
        /// </summary>
        private Task<UsuarioClaims> getClaims;

        /// <summary>
        /// initialize class.
        /// </summary> 
        /// <param name="mapper">Automapper</param>
        /// <param name="repoPlatos">Intance Repository Dishes<</param>
        /// <param name="repoRestaurantes">Intance Repository Restauramts<</param>
        /// <param name="roles">Intance Repository Roles<</param>
        /// <param name="useDishes">Use Case Dishes</param>

        public DishesService(IDishesRepository<Platos, string, int> repoPlatos, IRestaurantRespository<Restaurantes, int> repoRestaurantes, IRoles roles, IMapper mapper, IDishes useDishes)
        {
            this.repoDishes = repoPlatos;  
            this.repoRestaurant = repoRestaurantes;
            this.repoRoles = roles;
            this.mapper = mapper;
            //this.getClaims = this.repoRoles.getToken();
            this.useDishes = useDishes;
        }

        /// <summary>
        /// Add Dish.
        /// </summary> 
        /// <param name="entiyDTO">DishDTO</param>
        /// <returns>Dish Adedd</returns>
        public async Task<Platos> AddDish(PlatosDTO entiyDTO)
        {
            //useDishes.ValidateRol(getClaims);
            Platos dish = mapper.Map<Platos>(entiyDTO);          
            dish.Id = 0;
            dish.Activo = true;      
            var restauranteinfo = repoRestaurant.GetByID(dish.RestaurantesNIT_Id);
            useDishes.ValidateRestaurant(restauranteinfo, getClaims);
            var result = this.repoDishes.Add(dish);
            this.repoDishes.Confirm();
            return result;
        }

        /// <summary>
        /// Edit Dish.
        /// </summary> 
        /// <param name="entityDTO">DishDTO</param>
        /// <returns>Dish Edited</returns>
        public async Task<Platos> EditDish(PlatosDTO entityDTO)
        {
            Platos entidad = mapper.Map<Platos>(entityDTO);
            //useDishes.ValidateRol(getClaims);
            var restInfo = repoRestaurant.GetByID(entidad.RestaurantesNIT_Id);
            useDishes.ValidateRestaurant(restInfo, getClaims);      
            var select = repoDishes.GetByRestaurantNIT(entidad.NombrePlato, entidad.RestaurantesNIT_Id);
            useDishes.ValidateDish(select);
            select.Precio = entidad.Precio;
            select.Desacripcion = entidad.Desacripcion;
            select.Activo = entidad.Activo;
            repoDishes.Edit(select);
            repoDishes.Confirm();
            return select;
        }
    }
}
