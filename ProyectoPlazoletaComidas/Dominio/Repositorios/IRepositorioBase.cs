using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioBase<Tentidad, TentidadID>: IAgregar<Tentidad>, IEditar<Tentidad>, IEliminar<TentidadID>, IConsultar<Tentidad, TentidadID>, IConfirmar
    {
    }
}
