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
    /// <summary>
    /// Employee Restaurant DbSets   
    /// </summary> 
    public class EmployeeRestaurantsRepository : IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private Db_Context _context;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="context">DbContext.</param>
        public EmployeeRestaurantsRepository(Db_Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Employee Restaurant
        /// </summary>
        /// <param name="entity">User model</param>
        /// <returns>User Add</returns>
        public EmpleadosRestaurantes Add(EmpleadosRestaurantes entity)
        {
            _context.EmpleadosRestaurantes.Add(entity);
            return entity;
        }

        /// <summary>
        /// Save Chages
        /// </summary> 
        public void Confirm()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Get Empleoyee Restaurant by employee Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Empleoyee Restaurant</returns>
        public EmpleadosRestaurantes GetByID(int id)
        {
            return _context.EmpleadosRestaurantes.Where(x=> x.EmpleadoId == id).FirstOrDefault();
        }

        /// <summary>
        /// Get all Empleoyee Restaurant
        /// </summary>       
        /// <returns>list Empleoyee Restaurant</returns>
        public List<EmpleadosRestaurantes> GetAll()
        {
            return _context.EmpleadosRestaurantes.ToList();
        }
    }
}
