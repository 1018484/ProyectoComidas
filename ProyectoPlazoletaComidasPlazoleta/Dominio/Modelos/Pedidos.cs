using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class Pedidos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Pedido_Id { get; set; }

        [Required]
        public string Cliente_Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
        
        public int Chef_Id { get; set;}

        [Required]
        public int Estado { get; set; }

        [Required]
        [ForeignKey("Restaurantes")]
        public int RestaurantesNIT_Id { get; set; }

        public List<PedidosPlatos> PedidosPlatos { get; set; } 
        
    }
}
