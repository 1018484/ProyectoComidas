using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class SendOrder
    {
        public int RestauranteNIT { get; set; }

        public List<PlatosPedidosDTO> platos { get; set; }
    }
}
