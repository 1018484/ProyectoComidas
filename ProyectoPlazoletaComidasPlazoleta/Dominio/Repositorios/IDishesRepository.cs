using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IDishesRepository<Tentity, TentityID, TentityNIT> : IAdd<Tentity>, IEdit<Tentity, TentityID>, IConfirm, IGet<Tentity, TentityID>
    {
        Tentity GetByRestaurantNIT(TentityID id, TentityNIT nit);
        
    }
}
