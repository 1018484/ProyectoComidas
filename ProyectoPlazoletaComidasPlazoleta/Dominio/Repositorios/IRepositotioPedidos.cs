using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositotioPedidos<Tentidad, TetidadID> : IAgregar<Tentidad>, IConsultar<Tentidad, TetidadID>, IConfirmar
    {
        List<Tentidad> GetPedidos(int id);
    }
}
