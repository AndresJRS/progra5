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
    public class VehiculoesController : Controller
    {
        private ProyectoEntities1 db = new ProyectoEntities1();

        // GET: Vehiculoes
        public ActionResult Index()
        {
            return View(db.Vehiculos.ToList());
        }

        

        // GET: Vehiculoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehiculoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( RegistroVehiculos vehiculo)
        {
            Vehiculo v = new Vehiculo();


            v.Placa = vehiculo.Placa;
            v.Marca = vehiculo.Marca;
            v.Modelo = vehiculo.Modelo;
            v.Color = vehiculo.Color;


            if (ModelState.IsValid)
            {
                
                db.Vehiculos.Add(v);
         
                db.SaveChanges();
                Session["Vehiculo"] = v.IdVehiculo;


                return RedirectToAction("Create");
            }
            
            return View(v);
        }


     

        // GET: Vehiculoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistroVehiculos vehiculo)
        {
            return View();
        }

        // GET: Vehiculoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculo);
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
