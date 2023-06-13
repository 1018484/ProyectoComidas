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
        public bool PhoneValidation(string phone)
        {
            string Numero = phone;
            if (phone[0] == '+')
            {
                Numero = phone.Substring(3, phone.Length - 3);
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
    }
}