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
    public class DishesOrdersRepository : IDishesOrdersRepository<PedidosPlatos>
    {
        private Db_Context db_context;

        public DishesOrdersRepository(Db_Context context)
        {
            db_context = context;
        }

        public PedidosPlatos Add(PedidosPlatos entidad)
        {
            db_context.PedidosPlatos.Add(entidad);
            return entidad;
        }

        public void Confirm()
        {
           db_context.SaveChanges();
        }

        public List<PedidosPlatos> GetOrders(int id, int status) 
        {
            return db_context.PedidosPlatos.Include(p => p.Pedidos).Where(c => c.Pedidos.RestaurantesNIT_Id == id  && c.Pedidos.Estado == status).Include(p => p.Platos).ToList();            
        }
    }
}
