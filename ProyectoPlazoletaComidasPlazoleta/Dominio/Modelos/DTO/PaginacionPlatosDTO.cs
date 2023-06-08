using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos.DTO
{
    public class PaginacionPlatosDTO
    {
        public int NitRestaurante { get; set; }

        public List<CategoriasPlatos> CategoriasPlatos { get; set;}
        
    }
}
