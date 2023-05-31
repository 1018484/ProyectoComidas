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
    public class PlatosRepositotio : IRepositorioPlatos<Platos, string, int>
    {
        private Db_Context _context;

        public PlatosRepositotio(Db_Context _context)
        {
            this._context = _context;

        } 
        public Platos Agregar(Platos entidad)
        {
            this._context.Platos.Add(entidad);
            return entidad;
        }

        public void Confirmar()
        {
            this._context.SaveChanges();
        }

        public void Editar(Platos entidad)
        {
            this._context.Platos.Update(entidad);

        }

        public Platos obtener(string id)
        {
            return this._context.Platos.Where(p=> p.NombrePlato == id).FirstOrDefault();
        }

        public List<Platos> ObtenerTodos()
        {
            return this._context.Platos.ToList();
        }

        public Platos ConsultarPlatoPorRestaurante(string id, int nit)
        {

            return this._context.Platos.Where(p => p.NombrePlato == id && p.RestaurantesNIT_Id == nit).FirstOrDefault();
        }
    }
}
