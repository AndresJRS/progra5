using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ProyectoPrograV.Models;

namespace ProyectoPrograV.Controllers
{
    public class MecanicosController : Controller
    {
        [System.Web.Http.HttpPost]
        public ActionResult ActualizarEstado(int? idSolicitud)
        {
            int id = (int)Session["User"];
            var lista = new List<Solicitudes>();
            if (id != 0) {
               
                using (ProyectoEntities1 db = new ProyectoEntities1())
                {
                    var user = (from s in db.Solicitud
                                where s.idSolicitud == 1
                                select s).First();
                    user.Estado = 1;
                    db.SaveChanges();

                    var solicitudLista = from s in db.Solicitud
                                    join c in db.Clientes on s.idCliente equals c.idCliente
                                    join cv in db.ClienteVehiculoes on s.idCliente equals cv.idCliente
                                    join v in db.Vehiculos on cv.idVehiculo equals v.IdVehiculo
                                    where v.IdVehiculo == user.idVehiculo
                                    select new { nombre = c.Nombre, telefono = c.Telefono, detalle = s.Detalle, placa = v.Placa, marca = v.Marca , modelo = v.Modelo, color = v.Color};
                    
                    foreach (var solicitud in solicitudLista.ToList())
                    {
                        var modelo = new Solicitudes();
                        modelo.Nombre = solicitud.nombre;
                        modelo.Telefono = solicitud.telefono;
                        modelo.Detalle = solicitud.detalle;
                        modelo.Placa = solicitud.placa;
                        modelo.Marca = solicitud.marca;
                        modelo.Modelo = solicitud.modelo;
                        modelo.Color = solicitud.color;
                        modelo.id = user.idSolicitud;
                        lista.Add(modelo);
                    }
                }
            }

           
            return View(lista);
        }

        public ActionResult Arreglado()
        {
            return null;
        }
    }
}
