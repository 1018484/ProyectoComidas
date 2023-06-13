using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<PaginacionPedidos>> ListOrders(PedidsoFiltroDTO filtro);

        public void AssignOrder(List<Guid> orders);
    }
}
