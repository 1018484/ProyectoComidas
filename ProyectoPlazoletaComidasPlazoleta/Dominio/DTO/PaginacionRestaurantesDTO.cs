using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class PaginacionRestaurantesDTO
    {
        public int DatosPorPagina { get; set; }

        public int CantidadDePaginas { get; set; }

        public int NumeroDePagina { get; set; }

        public List<RestaurantesfiltradosDTO> Filtrados { get; set; }

    }
}
