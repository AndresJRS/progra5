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
        public ActionResult ActualizarEstado(int? idSolicitud)
        {
            var list = Session["User"].ToString();

            var id = int.Parse(list);

            using (ProyectoEntities1 db = new ProyectoEntities1()){
                var user = (from s in db.Solicitud
                            where s.idSolicitud == 1
                            select s).First();
                user.Estado = 1;
                db.SaveChanges();
            }
            return null;
        }
    }
}
