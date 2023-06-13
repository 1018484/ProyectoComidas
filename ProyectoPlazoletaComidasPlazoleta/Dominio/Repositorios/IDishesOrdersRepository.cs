using Dominio.Interfaces;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IDishesOrdersRepository<Tentity>: IAdd<Tentity>, IConfirm
    {     
        List<PedidosPlatos> GetOrders(int id, int status);
    }
}
