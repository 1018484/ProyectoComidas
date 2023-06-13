using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos.DTO
{
    public class PlatosDTO
    {
        public int Id { get; set; }
     
        public string NombrePlato { get; set; }
        
        public int Precio { get; set; }

        public string Desacripcion { get; set; }

        public string URLImagen { get; set; }

        public bool Activo { get; set; }

        public string Categoria { get; set; }

        public int RestaurantesNIT_Id { get; set; }
    }
}
