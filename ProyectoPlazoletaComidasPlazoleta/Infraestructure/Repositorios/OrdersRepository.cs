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
    public class OrdersRepository : IOrdersRepository<Pedidos, string>
    {
        private Db_Context db_context;

        public OrdersRepository(Db_Context context) 
        {   
            this.db_context = context;
        
        }
        public Pedidos Add(Pedidos entidad)
        {
            db_context.Pedidos.Add(entidad);
            return entidad;
        }

        public void Confirm()
        {
            db_context.SaveChanges();
        }

        public List<Pedidos> GetOrders(int id)
        {
            return db_context.Pedidos.Include(x => x.PedidosPlatos).Where(c => c.RestaurantesNIT_Id == id).ToList();
        }
        

        public Pedidos GetByID(string id)
        {
            return db_context.Pedidos.Where(x => x.Cliente_Id == id && (x.Estado == (int)EnumStatus.EnProceso || x.Estado == (int)EnumStatus.Pendiente || x.Estado == (int)EnumStatus.EnPreparacion || x.Estado == (int)EnumStatus.Listo)).FirstOrDefault();
        }
        public List<Pedidos> GetAll()
        {            
            return db_context.Pedidos.ToList(); 
        }

        public void Update(Guid order, int employeeID)
        {
            var select = db_context.Pedidos.Where(x=> x.Pedido_Id == order).FirstOrDefault();
            if (select == null)
            {
                throw new Exception("Invalid Order");
            }

            select.Chef_Id = employeeID;
            select.Estado = (int)EnumStatus.EnPreparacion;

            db_context.Pedidos.Update(select);
            db_context.SaveChanges();
        }
    }
}
