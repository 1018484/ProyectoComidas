using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class CambiarEstados
    {
        public Guid PedidoId { get; set; }  

        public int Estado { get; set;}

        public int Codigo { get; set; }
    }
}
