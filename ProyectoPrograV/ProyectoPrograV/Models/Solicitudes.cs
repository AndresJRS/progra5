using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPrograV.Models
{
    public class Solicitudes
    {
        public string Nombre{ get; set; }
        public string Telefono { get; set; }
        public string Detalle { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int id { get; set; }
    }
}