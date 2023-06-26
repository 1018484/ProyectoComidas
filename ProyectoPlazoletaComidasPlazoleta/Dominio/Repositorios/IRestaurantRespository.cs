using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRestaurantRespository<Tentity, TentityID>:IAdd<Tentity>, IConfirm, IGet<Tentity, TentityID>
    {        
        Tentity ObtenerById(int id);
    }
}
