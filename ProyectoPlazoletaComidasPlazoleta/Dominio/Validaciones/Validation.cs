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

        public string NumValidation(string dato)
        {
            if (string.IsNullOrEmpty(dato))
            {
                return string.Empty;
            }
            
            if (Regex.IsMatch(dato, @"^\d+$"))
            {
                return "Invalid Restaurant Name";
            }

            return string.Empty;
        }       
        
    }
}
