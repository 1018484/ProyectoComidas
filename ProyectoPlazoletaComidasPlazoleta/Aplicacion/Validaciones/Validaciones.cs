using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Validaciones
{
    public class Validaciones
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

        public bool ValidaTelefono(string telefono)
        {
            string Numero = telefono;
            if (telefono[0] == '+')
            {
                Numero = telefono.Substring(1, telefono.Length - 1);
                if (Numero.Length > 15)
                {
                    return false;
                }
            }

            if (Numero.Length > 13)
            {
                return false;
            }

            if (Regex.IsMatch(Numero, @"^\d+$"))
            {
                return true;
            }

            return false;
        }

        public bool ValidaNumerico(string dato)
        {
            bool result = false;
            if (Regex.IsMatch(dato, @"^\d+$"))
            {
                result = true;
            }

            return result;
        }

        public dynamic validartoken(ClaimsIdentity identity)
        {
            string rol = "0";
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return rol;
                }

                rol = identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value.ToString();
                return rol;

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);

            }

        }
    }
}
