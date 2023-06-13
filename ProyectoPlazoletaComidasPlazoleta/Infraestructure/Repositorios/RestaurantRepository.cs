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
    public class RestaurantRepository : IRestaurantRespository<Restaurantes, int>
    {
        private Db_Context db_context;

        public RestaurantRepository(Db_Context db_context)
        {
            this.db_context = db_context;
        }

        public Restaurantes Add(Restaurantes entidad)
        {
            db_context.Restaurantes.Add(entidad);
            return entidad;
        }

        public Restaurantes GetByID(int id)
        {            
            return db_context.Restaurantes.Where(u => u.NIT_Id == id).FirstOrDefault();
        }

        public List<Restaurantes> GetAll()
        {
            return db_context.Restaurantes.OrderBy(x=> x.Nombre).ToList();
        }

        public void Confirm()
        {
            db_context.SaveChanges();
        }

        public Restaurantes ObtenerById(int id)
        {
            return db_context.Restaurantes.Where(u => u.DocumentoId == id).FirstOrDefault();
        }
    }
}
