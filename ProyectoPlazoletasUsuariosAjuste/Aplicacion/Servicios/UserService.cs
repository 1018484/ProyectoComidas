using Aplicacion.Interfaces;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Validaciones;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios
{
    /// <summary>
    /// User Service Class.
    /// </summary>    
    public class UserService : IUserService    
    {
        /// <summary>
        /// Respository User DbSet.
        /// </summary> 
        private readonly IUserRespository repoUser;

        /// <summary>
        /// AutoMappeer.
        /// </summary> 
        private readonly IMapper mapper;

        /// <summary>
        /// AutoMappeer.
        /// </summary>
        private Aplicacion.Validaciones.Validation  validation;

        /// <summary>
        /// initialize class.
        /// </summary>   
        /// <param name="mapper">AutoMapper</param>
        /// <param name="repoUser">Repository User DbSets</param>
        public UserService(IUserRespository repoUser, IMapper mapper)
        {
            this.repoUser = repoUser;
            this.mapper = mapper;
            validation = new Aplicacion.Validaciones.Validation();
        }

        /// <summary>
        /// Add Owner
        /// </summary>
        /// <param name="entityDTO">User Dto</param> 
        public void AddOwner(UsuarioDTO entityDTO)
        {
            var entity = mapper.Map<Usuarios>(entityDTO);
            entity.RolesRolId = (int)EnumRoles.Propietario;
            validation.ValidateModel(entity);
            var result = repoUser.Add(entity);
            repoUser.Confirm();            
        }

        /// <summary>
        /// Add Emplyee
        /// </summary>
        /// <param name="entityDTO">User Dto</param> 
        public void AddEmployee(UsuarioDTO entityDTO)
        {
            var entity = mapper.Map<Usuarios>(entityDTO);
            entity.RolesRolId = (int)EnumRoles.Empleado;
            validation.ValidateModel(entity);
            var result = repoUser.Add(entity);          
            
        }

        /// <summary>
        /// Add client
        /// </summary>
        /// <param name="entityDTO">User Dto</param> 
        public void AddUser(UsuarioDTO entityDTO)
        {
            
            var entity = mapper.Map<Usuarios>(entityDTO);           
            entity.RolesRolId = (int)EnumRoles.Cliente;            
            validation.ValidateModel(entity);
            var result = repoUser.Add(entity);
            repoUser.Confirm();
            
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param> 
        /// <returns>User</returns>
        public Usuarios GetById(int id)
        {
            return repoUser.GetByID(id);
        }

        /// <summary>
        /// Valid Password
        /// </summary>
        /// <param name="userLoguin">User Login Dto</param> 
        /// <returns>User loggedin </returns>
        public Usuarios Password(UserLogin userLoguin)
        {
            return repoUser.ValidPassword(userLoguin);

        }
           
    }
}
