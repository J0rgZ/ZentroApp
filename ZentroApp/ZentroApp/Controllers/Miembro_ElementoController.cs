using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class Miembro_ElementoController : Controller
    {
        private readonly Miembro_Elemento miembroElemento = new Miembro_Elemento();

        // Método para cargar combos
        private void CargarCombos(int? idMiembro = null, int? idElemento = null)
        {
            using (var db = new ModeloGestion())
            {
                // Combo para miembros (mostrando el nombre del usuario asociado)
                var miembros = db.Miembro_Proyecto
                                 .Select(m => new
                                 {
                                     m.Id_miembro,
                                     NombreUsuario = m.Usuario.Nombre + " - Proyecto: " + m.Proyecto.Nombre
                                 })
                                 .ToList();

                ViewBag.Miembros = new SelectList(miembros, "Id_miembro", "NombreUsuario", idMiembro);

                // Combo para elementos de configuración
                var elementos = db.Elemento_Configuracion.ToList();
                ViewBag.Elementos = new SelectList(elementos, "Id_elementoconfiguracion", "Nombre", idElemento);
            }
        }

        // GET: MiembroElemento/Detalle/5
        public ActionResult Detalle(int id)
        {
            var item = miembroElemento.Obtener(id);
            if (item == null)
                return HttpNotFound();
            return View(item);
        }

        // GET: MiembroElemento/Nuevo
        public ActionResult Nuevo()
        {
            CargarCombos();
            return View();
        }

        // POST: MiembroElemento/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Miembro_Elemento modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            CargarCombos(modelo.Id_miembro, modelo.Id_elementoconfiguracion);
            return View(modelo);
        }

        // GET: MiembroElemento/Editar/5
        public ActionResult Editar(int id)
        {
            var item = miembroElemento.Obtener(id);
            if (item == null)
                return HttpNotFound();

            CargarCombos(item.Id_miembro, item.Id_elementoconfiguracion);
            return View(item);
        }

        // POST: MiembroElemento/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Miembro_Elemento modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            CargarCombos(modelo.Id_miembro, modelo.Id_elementoconfiguracion);
            return View(modelo);
        }

        // GET: MiembroElemento/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var item = miembroElemento.Obtener(id);
            if (item == null)
                return HttpNotFound();
            return View(item);
        }

        // POST: MiembroElemento/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var item = miembroElemento.Obtener(id);
            if (item != null)
            {
                item.Eliminar();
            }
            return RedirectToAction("Index");
        }
    }
}