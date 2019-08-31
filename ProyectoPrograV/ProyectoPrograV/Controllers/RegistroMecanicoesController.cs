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
    public class RegistroMecanicoesController : Controller
    {
        private ProyectoEntities1 db = new ProyectoEntities1();

        // GET: RegistroMecanicoes
        public ActionResult Index()
        {
            return View(db.Mecanicos.ToList());
        }

        // GET: RegistroMecanicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanico mecanico = db.Mecanicos.Find(id);
            if (mecanico == null)
            {
                return HttpNotFound();
            }
            return View(mecanico);
        }

        // GET: RegistroMecanicoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistroMecanicoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMecanico,Nombre,Email,Telefono,UsuarioMecanico,Contraseña")] Mecanico mecanico)
        {
            if (ModelState.IsValid)
            {
                db.Mecanicos.Add(mecanico);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(mecanico);
        }

        // GET: RegistroMecanicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanico mecanico = db.Mecanicos.Find(id);
            if (mecanico == null)
            {
                return HttpNotFound();
            }
            return View(mecanico);
        }

        // POST: RegistroMecanicoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMecanico,Nombre,Email,Telefono,UsuarioMecanico,Contraseña")] Mecanico mecanico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecanico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mecanico);
        }

        // GET: RegistroMecanicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanico mecanico = db.Mecanicos.Find(id);
            if (mecanico == null)
            {
                return HttpNotFound();
            }
            return View(mecanico);
        }

        // POST: RegistroMecanicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecanico mecanico = db.Mecanicos.Find(id);
            db.Mecanicos.Remove(mecanico);
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

        [System.Web.Http.HttpPost]
        public ActionResult ActualizarEstado(int? idSolicitud)
        {
            int id = (int)Session["User"];
            var lista = new List<Solicitudes>();
            if (id != 0)
            {

                using (ProyectoEntities1 db = new ProyectoEntities1())
                {
                    var user = (from s in db.Solicitud
                                where s.idSolicitud == idSolicitud
                                select s).First();
                    user.Estado = 1;
                    db.SaveChanges();

                    var solicitudLista = from s in db.Solicitud
                                         join c in db.Clientes on s.idCliente equals c.idCliente
                                         join cv in db.ClienteVehiculoes on s.idCliente equals cv.idCliente
                                         join v in db.Vehiculos on cv.idVehiculo equals v.IdVehiculo
                                         where v.IdVehiculo == user.idVehiculo
                                         select new { nombre = c.Nombre, telefono = c.Telefono, detalle = s.Detalle, placa = v.Placa, marca = v.Marca, modelo = v.Modelo, color = v.Color };

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

        public ActionResult Facturar(int? idSolicitud)
        {
            int id = (int)Session["User"];
            if (id != 0)
            {
                using (ProyectoEntities1 db = new ProyectoEntities1())
                {
                    var user = (from s in db.Solicitud
                                where s.idSolicitud == idSolicitud
                                select s).First();
                    user.Estado = 2;
                    db.SaveChanges();
                }
                
                Facturas(idSolicitud, id);
                
            }
            return RedirectToAction("Index", "Solicituds", "");
        }

        public void Facturas(int? idSoli, int id)
        {
            Random rnd = new Random();
            var factura = new Factura();
            using (ProyectoEntities1 db = new ProyectoEntities1())
            {
                var user = (from s in db.Solicitud
                            where s.idSolicitud == idSoli
                            select s).First();
                var solicitudLista = from s in db.Solicitud
                                     join c in db.Clientes on s.idCliente equals c.idCliente
                                     join cv in db.ClienteVehiculoes on s.idCliente equals cv.idCliente
                                     join v in db.Vehiculos on cv.idVehiculo equals v.IdVehiculo
                                     where v.IdVehiculo == user.idVehiculo
                                     select new { idcliente = c.idCliente, idsolicitud = s.idSolicitud, nombreCliente = c.Nombre};
                foreach (var solicitud in solicitudLista)
                {
                    factura.idCliente = solicitud.idcliente;
                    factura.idMecanico = user.idMecanico;
                    factura.idSolicitud = solicitud.idsolicitud;
                    factura.Monto = rnd.Next(10000,1000000);
                }

            }

            db.Facturas.Add(factura);
            db.SaveChanges();
        }

        public ActionResult ListaFacturas()
        {
            int id = (int)Session["User"];
            var lista = new List<ListaFacturas>();
            if (id != 0)
            {
                using (ProyectoEntities1 db = new ProyectoEntities1())
                {
                    var detalleFactura = from c in db.Clientes
                                         join f in db.Facturas on c.idCliente equals f.idCliente
                                         join cv in db.ClienteVehiculoes on c.idCliente equals cv.idCliente
                                         join v in db.Vehiculos on cv.idVehiculo equals v.IdVehiculo
                                         where f.idMecanico == id
                                         select new { idFactura = f.idFactura, nombreCliente = c.Nombre, marca = v.Marca, modelo = v.Modelo, f.Monto};
                    foreach (var factura in detalleFactura.ToList())
                    {
                        var model = new ListaFacturas() { idFactura = factura.idFactura, NombreCliente = factura.nombreCliente, MarcaCarro = factura.marca, ModeloCarro = factura.modelo, Monto = (double)factura.Monto };
                        lista.Add(model);
                    }
                }
            }
            return View(lista);
        }
    }
}
