using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IUserRepository<Tentidad, TentidadDTO, TentidadID>: IAdd<Tentidad>, IEdit<Tentidad>, IDelete<TentidadID>, IGet<Tentidad, TentidadDTO, TentidadID>, IConfirm
    {
    }
}
