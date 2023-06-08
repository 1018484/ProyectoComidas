using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IEmpleadosServicio
    {
        Task<List<Pedidos>> ListarPedidos(PedidsoFiltroDTO filtro);
    }
}
