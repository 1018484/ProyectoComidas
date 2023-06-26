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

namespace Infraestructure.Repositorios
{
    /// <summary>
    /// Dishes Repository DbSets   
    /// </summary>   
    public class DishesRepository : IDishesRepository<Platos, string, int>
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context db_context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="_context">DbContext.</param>
        public DishesRepository(Db_Context _context)
        {
            this.db_context = _context;

        }

        /// <summary>
        /// Add dish
        /// </summary>
        /// <param name="entity">Dish model</param>
        /// <returns>User Add</returns>
        public Platos Add(Platos entity)
        {
            this.db_context.Platos.Add(entity);
            return entity;
        }

        /// <summary>
        /// Save Chages
        /// </summary> 
        public void Confirm()
        {
            this.db_context.SaveChanges();
        }

        /// <summary>
        /// Save Chages
        /// </summary> 
        /// <param name="entity">Dish</param>
        public void Edit(Platos entity)
        {
            this.db_context.Platos.Update(entity);

        }

        /// <summary>
        /// Get by id
        /// </summary> 
        /// <param name="id">Dish ID</param>
        /// <returns>Dish</returns>
        public Platos GetByID(string id)
        {
            return this.db_context.Platos.Include(c=>c.PedidosPlatos).Where(p=> p.NombrePlato == id).FirstOrDefault();
        }

        /// <summary>
        /// Get all
        /// </summary>        
        /// <returns>to List Dish</returns>
        public List<Platos> GetAll()
        {
            return this.db_context.Platos.ToList();
        }

        /// <summary>
        /// Get dish by DishName and ResturantNIT_ID
        /// </summary> 
        /// <param name="id">Dish ID</param>
        /// <param name="nit">RestaurantNIT_ID</param>
        /// <returns>Dish</returns>
        public Platos GetByRestaurantNIT(string id, int nit)
        {
            return this.db_context.Platos.Where(p => p.NombrePlato == id && p.RestaurantesNIT_Id == nit).FirstOrDefault();
        }
    }
}
