using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

namespace Dominio.DTO
{
    public class PaginacionPlatos
    {
        public int DatosPorPagina { get; set; }

        public int CantidadDePaginas { get; set; }

        public int NumeroDePagina { get; set; }

        public List<Platos> Filtrados { get; set; }
    }
}
