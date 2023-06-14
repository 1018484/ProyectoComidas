using Dominio.Modelos;
using Dominio.Repositorios;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorios
{
    /// <summary>
    /// DishesOrder Repository DbSets   
    /// </summary>   
    public class DishesOrdersRepository : IDishesOrdersRepository<PedidosPlatos>
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context db_context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="context">DbContext.</param>
        public DishesOrdersRepository(Db_Context context)
        {
            db_context = context;
        }

        /// <summary>
        /// Add DishesOrder 
        /// </summary>
        /// <param name="entity">DishesOrder  model</param>
        /// <returns>DishesOrder </returns>
        public PedidosPlatos Add(PedidosPlatos entity)
        {
            db_context.PedidosPlatos.Add(entity);
            return entity;
        }

        /// <summary>
        /// Save Chages
        /// </summary> 
        public void Confirm()
        {
           db_context.SaveChanges();
        }

        /// <summary>
        /// Get DishesOrder by restaurantNIT_Id and Status
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="status">Status</param>
        /// <returns>list DishesOrder</returns>
        public List<PedidosPlatos> GetOrders(int id, int status) 
        {
            return db_context.PedidosPlatos.Include(p => p.Pedidos).Where(c => c.Pedidos.RestaurantesNIT_Id == id  && c.Pedidos.Estado == status).Include(p => p.Platos).ToList();            
        }
    }
}
