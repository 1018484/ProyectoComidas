using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Validaciones
{
    /// <summary>
    /// User validation Class.
    /// </summary>    
    public class Validation
    {
        /// <summary>
        /// phone number validation.
        /// </summary> 
        /// <param name="phone">Phone Number</param>
        /// <returns>Bool Valid phone</returns>
        public string PhoneValidation(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return string.Empty;
            }

            string Numero = phone;
            if (phone[0] == '+')
            {
                Numero = phone.Substring(3, phone.Length - 3);
            }

            if (Numero.Length > 13)
            {
                return "Invalid Phone";
            }

            if (Regex.IsMatch(Numero, @"^\d+$"))
            {
                return string.Empty;
            }

            return "Invalid Phone";
        }

        /// <summary>
        /// Valid Model.
        /// </summary> 
        /// <param name="phone">Phone Number</param>
        /// <returns>Bool Valid Model</returns>
        public bool ValidateModel(Usuarios user)
        {           
            string errorModel = PhoneValidation(user.Celular);
            var context = new ValidationContext(user, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, true);
            if (!isValid || !string.IsNullOrEmpty(errorModel))
            {
                foreach (var validationResult in results)
                {
                    errorModel= errorModel +"; "+validationResult.ErrorMessage;                    
                }

                throw new Exception($"Validation error: {errorModel}");
            }

            return isValid;
        }      
    }
}