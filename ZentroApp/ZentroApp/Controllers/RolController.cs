using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class RolController : Controller
    {
        private readonly Rol rol = new Rol();

        // GET: Rol
        // Mostrar todos los roles
        public ActionResult Index()
        {
            var lista = rol.Listar();
            return View(lista);
        }

        // GET: Rol/Detalle/5
        // Ver detalles de un rol
        public ActionResult Detalle(int id)
        {
            var item = rol.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Rol/Nuevo
        // Mostrar formulario para registrar un nuevo rol
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Rol/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Rol modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // GET: Rol/Editar/5
        // Mostrar formulario para editar un rol
        public ActionResult Editar(int id)
        {
            var item = rol.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Rol/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Rol modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // GET: Rol/Eliminar/5
        // Mostrar confirmación de eliminación
        public ActionResult Eliminar(int id)
        {
            var item = rol.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Rol/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var item = rol.Obtener(id);
            if (item != null)
            {
                item.Eliminar();
            }
            return RedirectToAction("Index");
        }
    }
}