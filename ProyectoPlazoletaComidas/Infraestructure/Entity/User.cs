﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public int DocumentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string Phone { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [ForeignKey("Rols")]
        public int RolsRolId { get; set; }

    }
}