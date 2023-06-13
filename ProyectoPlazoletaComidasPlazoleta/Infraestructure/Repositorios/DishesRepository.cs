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
    public class DishesRepository : IDishesRepository<Platos, string, int>
    {
        private Db_Context _context;

        public DishesRepository(Db_Context _context)
        {
            this._context = _context;

        } 
        public Platos Add(Platos entidad)
        {
            this._context.Platos.Add(entidad);
            return entidad;
        }

        public void Confirm()
        {
            this._context.SaveChanges();
        }

        public void Edit(Platos entidad)
        {
            this._context.Platos.Update(entidad);

        }

        public Platos GetByID(string id)
        {
            return this._context.Platos.Include(c=>c.PedidosPlatos).Where(p=> p.NombrePlato == id).FirstOrDefault();
        }

        public List<Platos> GetAll()
        {
            return this._context.Platos.ToList();
        }

        public Platos GetByRestaurantNIT(string id, int nit)
        {
            return this._context.Platos.Where(p => p.NombrePlato == id && p.RestaurantesNIT_Id == nit).FirstOrDefault();
        }
    }
}
