using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Validaciones
{
    public class Validation
    {
        public bool EmailValidation(string correo)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            Match match = regex.Match(correo);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PhoneValidation(string telefono)
        {
            string Numero = telefono;
            if (telefono[0] == '+')
            {
                Numero = telefono.Substring(3, telefono.Length - 3);                
            }

            if (Numero.Length > 13)
            {
                throw new Exception("Invalid Phone");
            }

            if (Regex.IsMatch(Numero, @"^\d+$"))
            {
                return true;
            }

            throw new Exception("Invalid Phone");
        }

        public bool NumValidation(string dato)
        {
            bool result = false;
            if (Regex.IsMatch(dato, @"^\d+$"))
            {
                throw new Exception("Invalid Restaurant Name");
            }

            return result;
        }       
        
    }
}
