using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPrograV.Models;

namespace ProyectoPrograV.Controllers
{
    public class SolicitudsController : Controller
    {
        private ProyectoEntities1 db = new ProyectoEntities1();

        // GET: Solicituds
        public ActionResult Index()
        {
            //0 solicitud pendiente y 1 solicitud completa
            using (ProyectoEntities1 db = new ProyectoEntities1())
            {
                var solicitud = from s in db.Solicitud
                                join c in db.Clientes on s.idCliente equals c.idCliente
                                where s.Estado == 0
                                select new { nombre = c.Nombre, telefono = c.Telefono, s.Detalle, s.idSolicitud };
                var lista = new List<Solicitudes>();
                foreach (var user in solicitud.ToList()) {
                    var modelo = new Solicitudes();
                    modelo.Nombre = user.nombre;
                    modelo.Telefono = user.telefono;
                    modelo.Detalle = user.Detalle;
                    modelo.id = user.idSolicitud;
                    lista.Add(modelo);
                }
                return View(lista);
            }
                //var solicitud = db.Solicitud.Include(s => s.Clientes).Include(s => s.Mecanicos);

        }

        // GET: Solicituds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: Solicituds/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nombre");
            ViewBag.idMecanico = new SelectList(db.Mecanicos, "idMecanico", "Nombre");
            return View();
        }

        // POST: Solicituds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSolicitud,idCliente,idMecanico,Estado,Detalle,idVehiculo,Ubicacion")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nombre", solicitud.idCliente);
            ViewBag.idMecanico = new SelectList(db.Mecanicos, "idMecanico", "Nombre", solicitud.idMecanico);
            return View(solicitud);
        }

        // GET: Solicituds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nombre", solicitud.idCliente);
            ViewBag.idMecanico = new SelectList(db.Mecanicos, "idMecanico", "Nombre", solicitud.idMecanico);
            return View(solicitud);
        }

        // POST: Solicituds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSolicitud,idCliente,idMecanico,Estado,Detalle,idVehiculo,Ubicacion")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nombre", solicitud.idCliente);
            ViewBag.idMecanico = new SelectList(db.Mecanicos, "idMecanico", "Nombre", solicitud.idMecanico);
            return View(solicitud);
        }

        // GET: Solicituds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
