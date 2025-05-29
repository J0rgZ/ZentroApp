using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class Elemento_ConfiguracionController : Controller
    {
        // Instancia del modelo
        private Elemento_Configuracion modelo = new Elemento_Configuracion();
        private Fase fase = new Fase();

        // GET: ElementoConfiguracion
        public ActionResult Index()
        {
            var lista = modelo.Listar();
            return View(lista);
        }

        // GET: ElementoConfiguracion/Crear
        public ActionResult Crear()
        {
            CargarFases();
            return View(new Elemento_Configuracion());
        }

        // POST: ElementoConfiguracion/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Elemento_Configuracion entidad)
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
                    ModelState.AddModelError("", "Error al guardar el elemento.");
                }
            }
            CargarFases();
            return View(entidad);
        }

        // GET: ElementoConfiguracion/Editar/5
        public ActionResult Editar(int id)
        {
            var entidad = modelo.Obtener(id);
            if (entidad == null)
                return HttpNotFound();

            CargarFases();
            return View(entidad);
        }

        // POST: ElementoConfiguracion/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Elemento_Configuracion entidad)
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
                    ModelState.AddModelError("", "Error al actualizar el elemento.");
                }
            }
            CargarFases();
            return View(entidad);
        }

        // GET: ElementoConfiguracion/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var entidad = modelo.Obtener(id);
            if (entidad == null)
                return HttpNotFound();

            return View(entidad);
        }

        // POST: ElementoConfiguracion/Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var entidad = modelo.Obtener(id);
            try
            {
                entidad.Eliminar();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el elemento.");
                return View(entidad);
            }
        }

        // GET: ElementoConfiguracion/Buscar
        public ActionResult Buscar(string criterio)
        {
            var resultados = modelo.Buscar(criterio);
            return View("Index", resultados);
        }

        // Método privado para cargar fases en el ComboBox
        private void CargarFases()
        {
            var fases = fase.Listar();
            ViewBag.Fases = new SelectList(fases, "Id_fase", "Nombre");
        }
    }
}