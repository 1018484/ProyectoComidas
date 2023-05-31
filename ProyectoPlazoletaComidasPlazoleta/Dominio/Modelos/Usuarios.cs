using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Modelos
{
    public class Usuarios
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public int DocumentoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }


        [Required]
        public string Celular { get; set; }


        [Required]
        public string Correo { get; set; }


        [Required]
        public string Clave { get; set; }

        [Required]
        [ForeignKey("Roles")]
        public int RolesRolId { get; set; }

        //List<RestauranteUsuario> RestauranteUsuario { get; set; }

        ////public Roles Roles { get; set; }

        ////public ICollection<Restaurantes> Restaurantes { get; set;}

    }
}
