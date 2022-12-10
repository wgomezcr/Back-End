using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO
    {
        //Se elimina el ID de la clase ya que no es necesario para las creaciones

        [Required]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }

        public string Biografia { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public IFormFile Foto { get; set; }

        //Proximos videos se revisa FOTO
        //public string Foto { get; set; }
    }
}
