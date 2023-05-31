using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IConsultar<Tentidad, TentidadID>
    {
        List<Tentidad> ObtenerTodos();

        Tentidad obtener(TentidadID id);
    }
}
