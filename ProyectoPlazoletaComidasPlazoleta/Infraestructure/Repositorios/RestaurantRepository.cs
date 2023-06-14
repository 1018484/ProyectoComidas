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
    /// <summary>
    /// Restaurant Repository DbSets    
    /// </summary> 
    public class RestaurantRepository : IRestaurantRespository<Restaurantes, int>
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context db_context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="db_context">DbContext.</param>
        public RestaurantRepository(Db_Context db_context)
        {
            this.db_context = db_context;
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="entity">User model</param>
        /// <returns>Restaurant Add</returns>
        public Restaurantes Add(Restaurantes entity)
        {
            db_context.Restaurantes.Add(entity);
            return entity;
        }

        /// <summary>
        /// Get restaurant by NIT_ID
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Restaurant</returns>
        public Restaurantes GetByID(int id)
        {            
            return db_context.Restaurantes.Where(u => u.NIT_Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Get all restaurant
        /// </summary>       
        /// <returns>List Restaurants</returns>
        public List<Restaurantes> GetAll()
        {
            return db_context.Restaurantes.OrderBy(x=> x.Nombre).ToList();
        }

        /// <summary>
        /// Save Chages
        /// </summary>
        public void Confirm()
        {
            db_context.SaveChanges();
        }

        /// <summary>
        /// Get restaurant by Owner_Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Restaurant</returns>
        public Restaurantes ObtenerById(int id)
        {
            return db_context.Restaurantes.Where(u => u.DocumentoId == id).FirstOrDefault();
        }
    }
}
