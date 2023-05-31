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
    public class RestauranteRepositorio : IRepositorioRestaurante<Restaurantes, int>
    {
        private Db_Context db_context;

        public RestauranteRepositorio(Db_Context db_context)
        {
            this.db_context = db_context;
        }

        public Restaurantes Agregar(Restaurantes entidad)
        {
            db_context.Restaurantes.Add(entidad);
            return entidad;
        }

        public Restaurantes obtener(int id)
        {
            return db_context.Restaurantes.Where(u => u.NIT_Id == id).FirstOrDefault();
        }

        public List<Restaurantes> ObtenerTodos()
        {
            return db_context.Restaurantes.ToList();
        }

        public void Confirmar()
        {
            db_context.SaveChanges();
        }
    }
}
