using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioPlatos<Tentidad, TentidadID, TentidadNIT> : IAgregar<Tentidad>, IEditar<Tentidad>, IConfirmar, IConsultar<Tentidad, TentidadID>
    {
        Tentidad ConsultarPlatoPorRestaurante(TentidadID id, TentidadNIT nit);
        
    }
}
