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
    /// Orders Repository DbSets   
    /// </summary> 
    public class OrdersRepository : IOrdersRepository<Pedidos, string>
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context db_context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="db_context">DbContext.</param>
        public OrdersRepository(Db_Context context) 
        {   
            this.db_context = context;        
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="entity">Order model</param>
        /// <returns>User Add</returns>
        public Pedidos Add(Pedidos entity)
        {
            db_context.Pedidos.Add(entity);
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
        /// Get order by OrderID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Order</returns>
        public Pedidos GetOrder(Guid id)
        {
            return db_context.Pedidos.Where(c => c.Pedido_Id == id).FirstOrDefault() ;
        }


        /// <summary>
        /// Get order by Client_ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Order</returns>
        public Pedidos GetByID(string id)
        {
            return db_context.Pedidos.Where(x => x.Cliente_Id == id && (x.Estado == (int)EnumStatus.EnProceso || x.Estado == (int)EnumStatus.Pendiente || x.Estado == (int)EnumStatus.EnPreparacion || x.Estado == (int)EnumStatus.Listo)).FirstOrDefault();
        }


        /// <summary>
        /// Get all orders 
        /// </summary>        
        /// <returns>list Orders</returns>
        public List<Pedidos> GetAll()
        {            
            return db_context.Pedidos.ToList(); 
        }

        /// <summary>
        /// Update Order orders 
        /// </summary>      
        /// <param name="code">OrderId</param>
        /// <param name="employeeID">EmployeeId</param>
        /// <param name="order">OrderId</param>
        /// <param name="status">Status</param>
        public void Update(Guid order, int employeeID, int status, int code)
        {
            var select = db_context.Pedidos.Where(x=> x.Pedido_Id == order).FirstOrDefault();
            if (select == null)
            {
                throw new Exception("Invalid Order");
            }

            select.Chef_Id = employeeID;
            select.Estado = status;  
            select.Codigo = code;

            db_context.Pedidos.Update(select);
            db_context.SaveChanges();
        }
    }
}
