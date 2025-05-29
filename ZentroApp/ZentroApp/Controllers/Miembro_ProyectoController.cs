using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class Miembro_ProyectoController : Controller
    {
        private readonly Miembro_Proyecto miembro = new Miembro_Proyecto();

        // Método para cargar combobox
        private void CargarCombos(int? idUsuario = null, int? idRol = null, int? idProyecto = null)
        {
            using (var db = new ModeloGestion())
            {
                ViewBag.Usuarios = new SelectList(db.Usuario.ToList(), "Id_usuario", "Nombre", idUsuario);
                ViewBag.Roles = new SelectList(db.Rol.ToList(), "Id_rol", "Nombre", idRol);
                ViewBag.Proyectos = new SelectList(db.Proyecto.ToList(), "Id_proyecto", "Nombre", idProyecto);
            }
        }

        // GET: MiembroProyecto
        public ActionResult Index()
        {
            var lista = miembro.Listar();
            return View(lista);
        }

        // GET: MiembroProyecto/Detalle/5
        public ActionResult Detalle(int id)
        {
            var item = miembro.Obtener(id);
            if (item == null)
                return HttpNotFound();
            return View(item);
        }

        // GET: MiembroProyecto/Nuevo
        public ActionResult Nuevo()
        {
            CargarCombos();
            return View();
        }

        // POST: MiembroProyecto/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Miembro_Proyecto modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            CargarCombos(modelo.Id_usuario, modelo.Id_rol, modelo.Id_proyecto);
            return View(modelo);
        }

        // GET: MiembroProyecto/Editar/5
        public ActionResult Editar(int id)
        {
            var item = miembro.Obtener(id);
            if (item == null)
                return HttpNotFound();
            CargarCombos(item.Id_usuario, item.Id_rol, item.Id_proyecto);
            return View(item);
        }

        // POST: MiembroProyecto/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Miembro_Proyecto modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Guardar();
                return RedirectToAction("Index");
            }
            CargarCombos(modelo.Id_usuario, modelo.Id_rol, modelo.Id_proyecto);
            return View(modelo);
        }

        // GET: MiembroProyecto/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var item = miembro.Obtener(id);
            if (item == null)
                return HttpNotFound();
            return View(item);
        }

        // POST: MiembroProyecto/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int id)
        {
            var item = miembro.Obtener(id);
            if (item != null)
            {
                item.Eliminar();
            }
            return RedirectToAction("Index");
        }
    }
}