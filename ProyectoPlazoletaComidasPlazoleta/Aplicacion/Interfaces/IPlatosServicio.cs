using Dominio.Interfaces;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IPlatosServicio<Tentidad, IDusuario>
    {
        Tentidad Agregar(Tentidad entidad, IDusuario IDusuario);

        void Editar(Tentidad entidad, IDusuario IDusuario);


    }
}
