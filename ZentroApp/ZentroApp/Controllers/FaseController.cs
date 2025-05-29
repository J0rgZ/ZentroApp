using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class FaseController : Controller
    {
        Fase fase = new Fase();

        // GET: Fase
        public ActionResult Index()
        {            
            var lista = fase.Listar();
            return View(lista);
        }

        // GET: Fase/Detalles/5
        public ActionResult Detalles(int id)
        {            
            var entidad = fase.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // GET: Fase/Crear
        public ActionResult Crear()
        {
            CargarMetodologiasDropDownList();
            return View();
        }

        // POST: Fase/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Fase entidad)
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
                    ModelState.AddModelError("", "Error al guardar la fase: " + ex.Message);
                }
            }
            CargarMetodologiasDropDownList(entidad.Id_metodologia);
            return View(entidad);
        }

        // GET: Fase/Editar/5
        public ActionResult Editar(int id)
        {
            var entidad = fase.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            CargarMetodologiasDropDownList(entidad.Id_metodologia);
            return View(entidad);
        }

        // POST: Fase/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Fase entidad)
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
                    ModelState.AddModelError("", "Error al actualizar la fase: " + ex.Message);
                }
            }
            CargarMetodologiasDropDownList(entidad.Id_metodologia);
            return View(entidad);
        }

        // GET: Fase/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var entidad = fase.Obtener(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // POST: Fase/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            try
            {                
                var entidad = fase.Obtener(id);
                if (entidad == null)
                {
                    return HttpNotFound();
                }
                entidad.Eliminar();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar la fase: " + ex.Message);
                var entidad = fase.Obtener(id);
                return View("Eliminar", entidad);
            }
        }

        private void CargarMetodologiasDropDownList(object selectedId = null)
        {
            var metodologiaModel = new Metodologia();
            var listaMetodologias = metodologiaModel.Listar()
                .OrderBy(m => m.Nombre)
                .ToList();
            ViewBag.Id_metodologia = new SelectList(listaMetodologias, "Id_metodologia", "Nombre", selectedId);
        }
    }
}