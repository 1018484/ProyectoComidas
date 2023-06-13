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
        public int DocumentoId { get; set; }
       
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Celular { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }
           
        public int RolesRolId { get; set; }       

    }
}
