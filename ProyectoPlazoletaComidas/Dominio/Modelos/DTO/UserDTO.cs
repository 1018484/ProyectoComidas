﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos.DTO
{
    public class UserDTO
    {
        public int DocumenId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }

        public int RolsRolId { get; set; }
    }
}