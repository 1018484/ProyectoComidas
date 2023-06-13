using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGet<Tentity, TentityID>
    {
        List<Tentity> GetAll();

        Tentity GetByID(TentityID id);
    }
}
