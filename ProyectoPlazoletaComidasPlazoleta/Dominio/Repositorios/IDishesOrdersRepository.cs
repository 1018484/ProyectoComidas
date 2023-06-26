using Dominio.Interfaces;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IDishesOrdersRepository<Tentity, TentityID>: IAdd<Tentity>, IConfirm, IDelete<TentityID>
        
    {     
        List<PedidosPlatos> GetOrders(int id, int status);
    }
}
