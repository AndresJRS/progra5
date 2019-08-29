using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPrograV.Models;

namespace ProyectoPrograV.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Enter(string usuario, string contraseña)
        {

            try
            {
                using (ProyectoEntities1 db = new ProyectoEntities1())
                {
                    
                    var lista = db.Mecanicos.Where(x => x.UsuarioMecanico.Equals(usuario) && x.Contraseña.Equals(contraseña)).ToList();

                    if (lista.Count() == 1)
                    {
                        var lst = new List<Sesiones>();
                        var idUsuario = from s in lista
                                        select s.idMecanico;
                        Session["User"] = idUsuario;
                        foreach (var user in lista)
                        {
                            var modelo = new Sesiones();
                            modelo.id = user.idMecanico;
                            modelo.usuario = user.UsuarioMecanico;
                            lst.Add(modelo);
                        }
                        return RedirectToAction("Index", "Solicituds", "");
                    }
                    else
                    {
                        return Content("Usuario invalido");
                    }

                }

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

    }
}