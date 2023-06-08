using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioRestauranteEmpleados<Tentidad, TentidadID> : IAgregar<Tentidad>, IConsultar<Tentidad, TentidadID>, IConfirmar
    {
        Task<int> GetrestauranteNIT(TentidadID id);
    }
}
