using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos.DTO
{
    public class CategoriasPlatos
    {
        public string Categoria { get; set; }

        public List<PaginacionPlatos> PaginacionPlatos { get; set; }
    }
}
