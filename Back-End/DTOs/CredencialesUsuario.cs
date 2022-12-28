﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class CredencialesUsuario
    {
        [EmailAddress]
        [Required]
        public String Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
