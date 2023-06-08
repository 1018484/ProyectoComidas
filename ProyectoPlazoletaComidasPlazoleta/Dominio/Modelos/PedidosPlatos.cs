using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class PedidosPlatos    
    {
        [ForeignKey("Pedidos")]        
        public Guid Pedido_Id { get; set;}

        public Pedidos Pedidos { get; set; }


        [ForeignKey("Platos")]
        public int Id { get; set; }     
        
        public Platos Platos { get; set; }  

        public int Cantidad { get; set; }
        
    }
}
