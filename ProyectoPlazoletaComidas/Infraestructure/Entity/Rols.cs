using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity
{
    public class Rols
    {
        [Key]
        public int RolId { get; set; }

        public string Rol { get; set; }

        public string Description { get; set; }

        public List<User> User { get; set; }
    }
}
