using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class RestauranteEmpleados
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public int EmpleadoId { get; set; }

        [Required]
        public int EmpleadorId { get; set; }

        [Required]
        public int RestauranteNIT_Id { get; set; }
    }
}
