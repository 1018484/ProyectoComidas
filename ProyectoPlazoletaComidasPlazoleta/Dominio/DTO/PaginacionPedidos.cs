using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class PaginacionPedidos
    {
        public Guid PedidoID { get; set; }
        
        public List<PedidosPlatosDTO> PedidosPlatosDTO { get; set; }

        
    }
}
