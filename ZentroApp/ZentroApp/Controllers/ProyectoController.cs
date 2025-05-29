using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly Proyecto proyecto = new Proyecto();

        // GET: Proyecto
        // Mostrar todos los proyectos (Index)
        public ActionResult Index()
        {
            var lista = proyecto.Listar();
            return View(lista);
        }

        // GET: Proyecto/Detalle/5
        // Mostrar detalles de un proyecto
        public ActionResult Detalle(int id)
        {
            var item = proyecto.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Proyecto/Nuevo
        // Mostrar formulario para crear un nuevo proyecto
        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Proyecto/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Proyecto modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // GET: Proyecto/Editar/5
        // Mostrar formulario para editar un proyecto
        public ActionResult Editar(int id)
        {
            var item = proyecto.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Proyecto/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Proyecto modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        // GET: Proyecto/Eliminar/5
        // Mostrar confirmación para eliminar un proyecto
        public ActionResult Eliminar(int id)
        {
            var item = proyecto.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Proyecto/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var item = proyecto.Obtener(id);
            if (item != null)
            {
                item.Eliminar();
            }
            return RedirectToAction("Index");
        }
    }
}