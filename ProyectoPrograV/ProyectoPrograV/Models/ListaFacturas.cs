using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPrograV.Models
{
    public class ListaFacturas
    {
        public int idFactura { get; set; }
        public string NombreCliente { get; set; }
        public string MarcaCarro { get; set; }
        public string ModeloCarro { get; set; }
        public double Monto { get; set; }
    }
}