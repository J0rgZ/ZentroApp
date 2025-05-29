using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class Tipo_UsuarioController : Controller
    {
        private Tipo_Usuario objTipo = new Tipo_Usuario();

        // GET: Tipo_Usuario
        public ActionResult Index()
        {
            return View(objTipo.Listar());
        }

        // GET: Tipo_Usuario/Detalle/5
        public ActionResult Detalle(int id)
        {
            return View(objTipo.Obtener(id));
        }

        // GET: Tipo_Usuario/Agregar
        public ActionResult Agregar()
        {
            return View();
        }

        // POST: Tipo_Usuario/Agregar
        [HttpPost]
        public ActionResult Agregar(Tipo_Usuario modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            modelo.Agregar();
            return RedirectToAction("Index");
        }

        // GET: Tipo_Usuario/Editar/5
        public ActionResult Editar(int id)
        {
            return View(objTipo.Obtener(id));
        }

        // POST: Tipo_Usuario/Editar
        [HttpPost]
        public ActionResult Editar(Tipo_Usuario modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            modelo.Editar();
            return RedirectToAction("Index");
        }

        // GET: Tipo_Usuario/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            return View(objTipo.Obtener(id));
        }

        // POST: Tipo_Usuario/Eliminar
        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmado(int id)
        {
            var tipo = objTipo.Obtener(id);
            tipo.Eliminar();
            return RedirectToAction("Index");
        }
    }
}