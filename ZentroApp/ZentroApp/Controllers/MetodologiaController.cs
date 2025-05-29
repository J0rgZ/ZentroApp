using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class MetodologiaController : Controller
    {
        Metodologia metodologia = new Metodologia();
        
        // GET: Metodologia
        public ActionResult Index()
        {
            
            var lista = metodologia.Listar();
            return View(lista);
        }

        // GET: Metodologia/Details/5
        public ActionResult Detalles(int id)
        {
            
            var entidad = metodologia.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // GET: Metodologia/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Metodologia/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Metodologia entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidad.Guardar();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la metodología: " + ex.Message);
                }
            }
            return View(entidad);
        }

        // GET: Metodologia/Editar/5
        public ActionResult Editar(int id)
        {
            var entidad = metodologia.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // POST: Metodologia/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Metodologia entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entidad.Guardar();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la metodología: " + ex.Message);
                }
            }
            return View(entidad);
        }

        // GET: Metodologia/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            
            var entidad = metodologia.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // POST: Metodologia/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            try
            {
                var entidad = metodologia.Obtener(id);
                if (entidad == null)
                {
                    return HttpNotFound();
                }
                entidad.Eliminar();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar la metodología: " + ex.Message);
                var entidad = metodologia.Obtener(id);
                return View("Eliminar", entidad);
            }
        }
    }
}