using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class CronogramaController : Controller
    {
        // Instancia de modelo
        private Cronograma cronograma = new Cronograma();
        private Proyecto proyecto = new Proyecto();

        // GET: Cronograma
        public ActionResult Index()
        {
            var lista = cronograma.Listar();
            return View(lista);
        }

        // GET: Cronograma/Crear
        public ActionResult Crear()
        {
            CargarProyectos();
            return View(new Cronograma { FechaInicio = DateTime.Now, Fecha_creacion = DateTime.Now });
        }

        // POST: Cronograma/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Cronograma entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidad.Fecha_creacion = DateTime.Now;
                    entidad.Guardar();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Error al guardar el cronograma.");
                }
            }

            CargarProyectos();
            return View(entidad);
        }

        // GET: Cronograma/Editar/5
        public ActionResult Editar(int id)
        {
            var entidad = cronograma.Obtener(id);
            if (entidad == null)
                return HttpNotFound();

            CargarProyectos();
            return View(entidad);
        }

        // POST: Cronograma/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cronograma entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidad.Guardar();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Error al actualizar el cronograma.");
                }
            }

            CargarProyectos();
            return View(entidad);
        }

        // GET: Cronograma/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var entidad = cronograma.Obtener(id);
            if (entidad == null)
                return HttpNotFound();

            return View(entidad);
        }

        // POST: Cronograma/Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var entidad = cronograma.Obtener(id);
            try
            {
                entidad.Eliminar();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el cronograma.");
                return View(entidad);
            }
        }

        // GET: Cronograma/Buscar
        public ActionResult Buscar(string criterio)
        {
            var resultados = cronograma.Buscar(criterio);
            return View("Index", resultados);
        }

        // Método privado para cargar proyectos en un combo
        private void CargarProyectos()
        {
            var proyectos = proyecto.Listar();
            ViewBag.Proyectos = new SelectList(proyectos, "Id_proyecto", "Nombre");
        }
    }
}