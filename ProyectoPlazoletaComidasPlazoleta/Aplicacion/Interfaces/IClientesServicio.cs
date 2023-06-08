using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{      
    public interface IClientesServicio
    {
        List<PaginacionRestaurantesDTO> ListarRestaurantes(int paginacion);

        List<PaginacionPlatosDTO> ListarPlatos(int paginacion);

        Task AgregarPedidos(PedidosDTO pedido);
        
    }
}
