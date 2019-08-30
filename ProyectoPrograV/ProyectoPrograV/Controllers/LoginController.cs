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
                    var lista = db.Clientes.Where(x => x.UsuarioCliente.Equals(usuario) && x.Contraseña.Equals(contraseña)).ToList();


                    if (lista.Count() == 1)
                    {

                        var idUsuario = (from s in lista
                                        select s.idCliente).First();
                        Session["User"] = idUsuario;


                        return RedirectToAction("Create", "Solicituds", "");
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