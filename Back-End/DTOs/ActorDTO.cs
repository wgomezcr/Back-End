﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Biografia { get; set; }

        public DateTime FechaNacimineto { get; set; }

        public string Foto { get; set; }
    }
}
