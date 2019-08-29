using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPrograV.Models;

namespace ProyectoPrograV.Controllers
{
    public class LoginCController : Controller
    {
        // GET: LoginC
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
                        var lst = new List<Sesiones>();
                        var idUsuario = from s in lista
                                        select s.idCliente;
                        Session["User"] = lista.First();
                        foreach (var user in lista)
                        {
                            var modelo = new Sesiones();
                            modelo.id = user.idCliente;
                            modelo.usuario = user.UsuarioCliente;
                            lst.Add(modelo);
                        }
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