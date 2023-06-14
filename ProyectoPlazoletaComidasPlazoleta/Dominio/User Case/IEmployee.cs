using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    public interface IEmployee
    {
        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        void ValidateRol(Task<UsuarioClaims> claims);

        /// <summary>
        /// order paging
        /// </summary>  
        /// <param name="ordersDishes">User logged in</param>
        /// <returns>List order paging</returns>
        List<PaginacionPedidos> pageOders(IEnumerable<IGrouping<Guid,PedidosPlatos>> ordersDishes);
    }
}
