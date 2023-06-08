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
    public class PedidosRepositorio : IRepositotioPedidos<Pedidos, string>
    {
        private Db_Context db_context;

        public PedidosRepositorio(Db_Context context) 
        {   
            this.db_context = context;
        
        }
        public Pedidos Agregar(Pedidos entidad)
        {
            db_context.Pedidos.Add(entidad);
            return entidad;
        }

        public void Confirmar()
        {
            db_context.SaveChanges();
        }

        public List<Pedidos> GetPedidos(int id)
        {          

            return  db_context.Pedidos.Include(x=> x.PedidosPlatos).Where(c=> c.RestaurantesNIT_Id == id).ToList();           
        }

        public Pedidos obtener(string id)
        {
            return db_context.Pedidos.Where(x => x.Cliente_Id == id && (x.Estado == (int)EnumEstados.EnProceso || x.Estado == (int)EnumEstados.Pendiente || x.Estado == (int)EnumEstados.EnPreparacion || x.Estado == (int)EnumEstados.Listo)).FirstOrDefault();
        }
        public List<Pedidos> ObtenerTodos()
        {            
            return db_context.Pedidos.ToList(); 
        }
    }
}
