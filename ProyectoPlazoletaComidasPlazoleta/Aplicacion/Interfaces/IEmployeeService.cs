using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// IEmployee Service 
    /// </summary>    
    public interface IEmployeeService
    {
        /// <summary>
        /// to List Orders by restaurant and Status
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>List orders</returns>
        Task<List<PaginacionPedidos>> ListOrders(PedidsoFiltroDTO filter);

        /// <summary>
        /// Assing orders to employee
        /// </summary>
        /// <param name="orders">List orders to assign</param>
        public void AssignOrder(List<Guid> orders);

        /// <summary>
        /// change order status 
        /// </summary>
        /// <param name="dto">Status</param>
        Task StatusAsync(CambiarEstados dto);
    }
}
