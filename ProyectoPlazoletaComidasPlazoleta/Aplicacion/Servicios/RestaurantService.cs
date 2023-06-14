using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Repositorios;
using System.Text.RegularExpressions;
using Aplicacion.Validaciones;
using AutoMapper;
using Dominio.DTO;
using Dominio.User_Case;

namespace Applicacion.Repositorio
{
    /// <summary>
    /// Restaurant Service Class.
    /// </summary>
    public class RestaurantService : IRestarurantService
    {
        /// <summary>
        /// Repository Restaurant DbSet
        /// </summary>
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;

        /// <summary>
        /// Repository Get User Remoto httpCLient
        /// </summary>
        private readonly IUsersRemotoRepository<Usuarios, int> repoUerRemoto;

        /// <summary>
        /// Repository Valid token and sesion
        /// </summary>
        private readonly IRoles repoRoles;

        /// <summary>
        /// Repository EmployeeRestaurant BbSet
        /// </summary>
        private readonly IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> repoEmployeeRestaurant;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Use Case Restaurant
        /// </summary>
        private readonly IRestaurant useRestaurant;

        /// <summary>
        /// Validations
        /// </summary>
        private Validation Validation;

        /// <summary>
        /// User sesion
        /// </summary>
        private Task<UsuarioClaims> getClaims;

        /// <summary>
        /// initialize class.
        /// </summary>   
        /// <param name="mapper">AutoMapper</param>        
        /// <param name="employeeRestaurant">Repository Employee Restaurant DbSet</param>
        /// <param name="repoRestaurante">Repository Restaurant DbSet</param>
        /// <param name="repoRoles">Reepository Sesion and user</param>
        /// <param name="repoUsuario">Repository user Remoto</param>
        /// <param name="useRestaurant">Use case</param>
        public RestaurantService(IRestaurantRespository<Restaurantes, int> repoRestaurante, IUsersRemotoRepository<Usuarios, int> repoUsuario, IRoles repoRoles, IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> employeeRestaurant, IMapper mapper, IRestaurant useRestaurant)
        {
            this.repoRestaurant = repoRestaurante;
            this.repoUerRemoto = repoUsuario;
            Validation = new Validation();
            this.repoRoles = repoRoles;
            this.mapper = mapper;
            this.repoEmployeeRestaurant = employeeRestaurant;            
            this.getClaims =  this.repoRoles.getToken();       
            this.useRestaurant = useRestaurant;
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="entityDTO">Restaurant DTO</param>
        /// <returns>Restaurant Creeated</returns>
        public async Task<Restaurantes> AddRestaurant(RestaurantesDTO entityDTO)
        {            
            useRestaurant.ValidateRol(getClaims);
            Restaurantes restaurant = mapper.Map<Restaurantes>(entityDTO);
            Validation.PhoneValidation(restaurant.Telefono);
            Validation.NumValidation(restaurant.Nombre);    
            var usuario = await repoUerRemoto.GetUserID(restaurant.DocumentoId);
            useRestaurant.ValidateUser(usuario);           
            var result = repoRestaurant.Add(restaurant);
            this.repoRestaurant.Confirm();
            return result;
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="ownerID">Owner ID</param> 
        public async Task AddEmployeeRestaurant(int ownerID)
        {
            useRestaurant.ValidateRol(getClaims);
            EmpleadosRestaurantes employeerestaurant = new EmpleadosRestaurantes()
            {
                EmpleadoId = ownerID,
                RestauranteNIT = repoRestaurant.ObtenerById(int.Parse(getClaims.Result.Id)).NIT_Id 
            };

            repoEmployeeRestaurant.Add(employeerestaurant);
            repoEmployeeRestaurant.Confirm();
        }        
    }
}
