using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class Roles
    {
        [Key]
        public int RolId { get; set; }
        
        public string Rol { get; set; }
       
        public string Descripcion { get; set; }   
        
        public List<Usuarios> Usuarios { get; set; }

    }
}
