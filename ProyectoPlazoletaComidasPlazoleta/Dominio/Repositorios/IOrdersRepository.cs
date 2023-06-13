using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IOrdersRepository<Tentity, TentityID> : IAdd<Tentity>, IGet<Tentity, TentityID>, IConfirm
    {
        List<Tentity> GetOrders(int id);

        void Update(Guid Order, int employeeID);
    }
}
