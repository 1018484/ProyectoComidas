using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTO
{
    public class PedidosPlatosDTO
    {      
        public PedidosDTO Pedidos { get; set; }
       
        public int Id { get; set; }

        public PlatosDTO Platos { get; set; }

        public int Cantidad { get; set; }
    }
}
