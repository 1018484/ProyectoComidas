using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Modelos;

namespace Applicacion.Interfaces
{
    //public interface IUsuarioServicio<Tentidad, TentidadID>: IConsultar<Tentidad, TentidadID>//IAgregar<Tentidad>, IConsultar<Tentidad, TentidadID>
    //                                                                                      //, IEditar<Tentidad>, IEliminar<TentidadID>
    //{
    //    Tentidad AgregarPropietario(Tentidad entidad);

    //    Tentidad AgregarEmpleado(Tentidad entidad);

    //    Tentidad AgregarUsuario(Tentidad entidad);

    //}

    public interface IUsuarioServicio
    {
        Usuarios AgregarPropietario(Usuarios entidad);

        Usuarios AgregarEmpleado(Usuarios entidad);

        Usuarios AgregarUsuario(Usuarios entidad);

        List<Usuarios> ObtenerTodos();

        Usuarios obtener(int id);

    }
}
