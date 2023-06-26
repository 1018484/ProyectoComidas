using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class EmpleadosRestaurantes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int EmpleadoId { get; set; }

        [Required]
        public int RestauranteNIT { get; set; }
    }
}
