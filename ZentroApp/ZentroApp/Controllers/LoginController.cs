using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Usuario usuario = new Usuario();
                var user = usuario.ObtenerUsuarioPorCredenciales(model.Username, model.Password);

                // CORRECCIÓN: Usar constante para verificar estado activo
                if (user != null && user.Estado == Usuario.ESTADO_ACTIVO)
                {
                    Session["usuario"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string errorMessage = "Credenciales incorrectas o usuario inactivo";

                    if (user != null && user.Estado != Usuario.ESTADO_ACTIVO)
                    {
                        errorMessage = $"Usuario inactivo (Estado: {user.Estado})";
                    }

                    ModelState.AddModelError("", errorMessage);
                    return View(model);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error interno del sistema");
                return View(model);
            }
        }

        // Cerrar sesión
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}