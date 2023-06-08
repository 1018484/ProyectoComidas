using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos.DTO
{
    public class PedidosDTO
    {
        public int RestauranteNIT { get; set; }

        public List<PlatosPedidosDTO> platos { get; set;}
    }
}
