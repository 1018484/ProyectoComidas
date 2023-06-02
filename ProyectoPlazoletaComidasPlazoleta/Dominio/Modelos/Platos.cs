using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class Platos
    {     

        [Key]
        public int Id { get; set; }

        [Required]
        public string NombrePlato { get; set; }

        [Required]
        public int Precio { get; set; }

        [Required]
        public string Desacripcion { get; set;}

        [Required]
        public string URLImagen { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        [ForeignKey("Restaurantes")]
        public int RestaurantesNIT_Id { get; set; }

    }
}
