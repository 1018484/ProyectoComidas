using Aplicacion.Validaciones;
using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.User_Case
{
    /// <summary>
    /// User Case Restaurant Class.
    /// </summary> 
    public class Restaurant : IRestaurant
    {       

        /// <summary>
        /// Validate Rol and Sesion.
        /// </summary>  
        /// <param name="claims">User logged in</param>
        public void ValidateRol(Task<UsuarioClaims> claims)
        {
            if (claims == null)
            {
                throw new Exception("Session expired or session not started");
            }

            if (int.Parse(claims.Result.Rol) != (int)EnumRoles.Administrador)
            {
                throw new Exception("User Not authorized");
            }
        }

        /// <summary>
        /// Validate user.
        /// </summary>  
        /// <param name="user">user</param>
        public void ValidateUser(Usuarios user)
        {
            if (user == null)
            {
                throw new Exception("You are trying to add a restaurant to an unregistered User");
            }

            if (user.RolesRolId != (int)EnumRoles.Propietario)
            {
                throw new Exception("You are trying to add a restaurant to a User who does not have permissions to have one");
            }
        }

        /// <summary>
        /// Validate Model.
        /// </summary>  
        /// <param name="rest">user</param>
        public void ValidateModel(Restaurantes rest)
        {
            Validation val = new Validation();
            string errorModel = val.PhoneValidation(rest.Telefono) + val.NumValidation(rest.Nombre);
            var context = new ValidationContext(rest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(rest, context, results, true);
            if (!isValid || !string.IsNullOrEmpty(errorModel))
            {
                foreach (var validationResult in results)
                {
                    errorModel = errorModel + "; " + validationResult.ErrorMessage;
                }

                throw new Exception($"Validation error: {errorModel}");
            }
        }


    }
}
