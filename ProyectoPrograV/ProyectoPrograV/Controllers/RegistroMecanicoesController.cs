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
    }
}
