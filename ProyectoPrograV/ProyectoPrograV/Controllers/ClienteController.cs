using ProyectoPrograV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPrograV.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrearSolicitud()
        {
            var lst = Session["User"] as List<Sesiones>;
            foreach (Sesiones item in lst)
            {
                var modelo = new Sesiones();
                item.id = modelo.id;
                item.usuario = modelo.usuario;
                lst.Add(modelo);
            }
            return View(Session["User"]);
        }
    }

}