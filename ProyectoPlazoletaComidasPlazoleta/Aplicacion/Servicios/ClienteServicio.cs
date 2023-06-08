using Aplicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ClienteServicio : IClientesServicio
    {
        private readonly IRepositorioRestaurante<Restaurantes, int> repoRestaurantes;

        private readonly IRepositorioPlatos<Platos, string, int> repoPlatos ;

        private readonly IRepositotioPedidos<Pedidos, string> repoPedidos ;

        private readonly IRoles repoRoles;

        private readonly IRepositorioPedidosPlatos<PedidosPlatos> repoPedidosPlatos ;


        public ClienteServicio(IRepositorioRestaurante<Restaurantes, int> retaurante, IRepositorioPlatos<Platos, string, int> platos, IRepositotioPedidos<Pedidos, string> pedidos, IRoles repoRoles, IRepositorioPedidosPlatos<PedidosPlatos> repoPedidosPlatos)
        {
            this.repoRestaurantes = retaurante;
            this.repoPlatos = platos;
            this.repoPedidos = pedidos;
            this.repoRoles = repoRoles;
            this.repoPedidosPlatos = repoPedidosPlatos;            
        }

        public async Task AgregarPedidos(PedidosDTO pedidoDTO)
        {            
            var getClaims = await repoRoles.getToken();
            if (getClaims == null)
            {
                throw new Exception("La sesion caduco o el usuario no iniciado sesion");
            }

            if (Enum.Parse<EnumRoles>(getClaims.Rol)  != EnumRoles.Cliente)
            {
                throw new Exception("El Usuario no tiene permiso para solicitar pedidos");
            }

            var ValidaPedidosEnCurso = this.repoPedidos.obtener(getClaims.Id);
            if (ValidaPedidosEnCurso != null)
            {
                throw new Exception("El Usuario no puede crear otro pedido hasta que no finalicen sus pedidos en cola");
            }

            Pedidos pedido = new Pedidos();
            pedido.Pedido_Id = Guid.NewGuid();
            pedido.Cliente_Id = getClaims.Id;
            pedido.Fecha = DateTime.Now;            
            pedido.Estado = (int)EnumEstados.Pendiente;
            pedido.RestaurantesNIT_Id = pedidoDTO.RestauranteNIT;
            this.repoPedidos.Agregar(pedido);
            this.repoPedidos.Confirmar();
            AgendarPlatos(pedido.Pedido_Id, pedidoDTO.platos);

        }

        public List<PaginacionPlatosDTO> ListarPlatos(int paginacion)
        {
            int paginas = 0;
            List<PaginacionPlatosDTO> result = new List<PaginacionPlatosDTO>();            
            var RestaurantesGroup = repoPlatos.ObtenerTodos().GroupBy(x => x.RestaurantesNIT_Id);                 
            foreach (var restaurante in RestaurantesGroup)
            {
                PaginacionPlatosDTO dto = new PaginacionPlatosDTO();
                dto.CategoriasPlatos = new List<CategoriasPlatos>();
                dto.NitRestaurante = restaurante.Key;
                var platosgroup = restaurante.GroupBy(x => x.Categoria);               
                foreach (var plato in platosgroup)                
                {   
                    CategoriasPlatos categoriasPlatos = new CategoriasPlatos();
                    categoriasPlatos.PaginacionPlatos = new List<PaginacionPlatos>();
                    categoriasPlatos.Categoria = plato.Key;
                    IEnumerable<IGrouping<int, Platos>> groups = plato.GroupBy(x=> x.RestaurantesNIT_Id);
                    IEnumerable<Platos> platos = groups.SelectMany(group => group);
                    List<Platos> listaPlatos = new List<Platos>();
                    listaPlatos = platos.ToList();
                    paginas = (int)listaPlatos.Count() / paginacion;
                    if (listaPlatos.Count() % paginacion != 0)
                    {
                        paginas += 1;
                    }

                    for (int i = 1; i <= paginas; i++)
                    {
                        PaginacionPlatos paginacionPlatos = new PaginacionPlatos();                        
                        paginacionPlatos.DatosPorPagina = paginacion;
                        paginacionPlatos.CantidadDePaginas = paginas;
                        paginacionPlatos.NumeroDePagina = i;
                        paginacionPlatos.Filtrados = new List<Platos>();
                        for (int a = 1; a <= paginacion; a++)
                        {
                            if (listaPlatos.Count == 0)
                            {
                                break;
                            }

                            paginacionPlatos.Filtrados.Add(listaPlatos[0]);
                            listaPlatos.Remove(listaPlatos[0]);
                        }

                        categoriasPlatos.PaginacionPlatos.Add(paginacionPlatos);
                    }

                    dto.CategoriasPlatos.Add(categoriasPlatos);
                }

                result.Add(dto);
            }
            foreach (var r in result) 
            {
                if (r.CategoriasPlatos[0].Categoria == "hamburguesas")
                {
                    var t = r.CategoriasPlatos[0].PaginacionPlatos[0].Filtrados;
                }
            }
            
            return result;               
        }

        public List<PaginacionRestaurantesDTO> ListarRestaurantes(int paginacion)
        {
            int paginas = 0;
            List<PaginacionRestaurantesDTO> paginacionlist = new List<PaginacionRestaurantesDTO> ();
            var restaurantes = repoRestaurantes.ObtenerTodos().Select(x=> new RestaurantesfiltradosDTO()
            {
                Nombre = x.Nombre,
                URLLogo = x.URLLogo
            }).ToList();

            paginas = (int)restaurantes.Count() / paginacion;
            if (restaurantes.Count() % paginacion != 0 )
            {
                paginas += 1;
            }

            for (int i =1; i<= paginas; i++)
            {
                PaginacionRestaurantesDTO pretaurantePorPagina = new PaginacionRestaurantesDTO();
                pretaurantePorPagina.DatosPorPagina = paginacion;
                pretaurantePorPagina.CantidadDePaginas = paginas;
                pretaurantePorPagina.NumeroDePagina = i;
                pretaurantePorPagina.Filtrados = new List<RestaurantesfiltradosDTO>();
                for (int a = 1; a<= paginacion; a++)
                {
                    if (restaurantes.Count == 0)
                    {
                        break;
                    }

                    pretaurantePorPagina.Filtrados.Add(restaurantes[0]);
                    restaurantes.Remove(restaurantes[0]);
                }

                paginacionlist.Add(pretaurantePorPagina);
            }

            return paginacionlist;
        }
        
        public void AgendarPlatos(Guid id, List<PlatosPedidosDTO> platos)
        {
            foreach (var plato in platos)
            {
                PedidosPlatos pedidosPlatos = new PedidosPlatos()
                {
                    Pedido_Id = id,
                    Id = plato.IdPlato,
                    Cantidad=plato.Cantidad 
                };

                repoPedidosPlatos.Agregar(pedidosPlatos);
                repoPedidosPlatos.Confirmar();
            }
        }
    }
}
