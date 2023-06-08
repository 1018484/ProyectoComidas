using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class Restaurantes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int NIT_Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string URLLogo { get; set; }

        [Required]
        public int DocumentoId { get; set; }
        

        public List<Platos> platos { get; set; }       

        public List<Pedidos> pedidos { get; set; }
        

    }
}
