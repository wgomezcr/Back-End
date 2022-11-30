using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class CineDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }


        //[Range(-90,90)]
        public double Latitud { get; set; }

        //[Range(180, -180)]
        public double Longitud { get; set; }
    }
}
