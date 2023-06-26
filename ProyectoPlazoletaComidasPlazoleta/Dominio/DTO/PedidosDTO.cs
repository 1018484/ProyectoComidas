using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class PedidosDTO
    {         
        public string Cliente_Id { get; set; }
        
        public DateTime Fecha { get; set; }

        public int Chef_Id { get; set; }

        public int Estado { get; set; }
        
        public int RestaurantesNIT_Id { get; set; }

    }
}
