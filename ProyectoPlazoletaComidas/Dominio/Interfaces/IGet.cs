using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGet<Tentidad, TentidadDTO, TentidadID>
    {
        List<TentidadDTO> GetAll();

        TentidadDTO GetAllById(TentidadID id);
    }
}
