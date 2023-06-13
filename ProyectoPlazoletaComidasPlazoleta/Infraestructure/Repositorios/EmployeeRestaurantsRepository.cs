using Dominio.Modelos;
using Dominio.Repositorios;
using infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorios
{
    public class EmployeeRestaurantsRepository : IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>
    {
        private Db_Context _context;

        public EmployeeRestaurantsRepository(Db_Context context)
        {
            _context = context;
        }
        public EmpleadosRestaurantes Add(EmpleadosRestaurantes entidad)
        {
            _context.EmpleadosRestaurantes.Add(entidad);
            return entidad;
        }

        public void Confirm()
        {
            _context.SaveChanges();
        }

        public EmpleadosRestaurantes GetByID(int id)
        {
            return _context.EmpleadosRestaurantes.Where(x=> x.EmpleadoId == id).FirstOrDefault();
        }

        public List<EmpleadosRestaurantes> GetAll()
        {
            return _context.EmpleadosRestaurantes.ToList();
        }
    }
}
