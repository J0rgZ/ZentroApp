using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly Solicitud solicitud = new Solicitud();

        // GET: Solicitud
        public ActionResult Index()
        {
            var lista = solicitud.Listar();
            return View(lista);
        }

        // GET: Solicitud/Details/5
        public ActionResult Detalles(int id)
        {
            var item = solicitud.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Solicitud/Create
        public ActionResult Crear()
        {
            ViewBag.Usuarios = new SelectList(new ModeloGestion().Usuario.ToList(), "Id_usuario", "Nombre");
            return View();
        }

        // POST: Solicitud/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Solicitud model)
        {
            if (ModelState.IsValid)
            {
                model.Fecha_creacion = DateTime.Now;
                model.Guardar();
                return RedirectToAction("Index");
            }

            ViewBag.Usuarios = new SelectList(new ModeloGestion().Usuario.ToList(), "Id_usuario", "Nombre", model.Id_usuario_solicitante);
            return View(model);
        }

        // GET: Solicitud/Edit/5
        public ActionResult Editar(int id)
        {
            var item = solicitud.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.Usuarios = new SelectList(new ModeloGestion().Usuario.ToList(), "Id_usuario", "Nombre", item.Id_usuario_solicitante);
            return View(item);
        }

        // POST: Solicitud/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Solicitud model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return RedirectToAction("Index");
            }

            ViewBag.Usuarios = new SelectList(new ModeloGestion().Usuario.ToList(), "Id_usuario", "Nombre", model.Id_usuario_solicitante);
            return View(model);
        }

        // GET: Solicitud/Delete/5
        public ActionResult Eliminar(int id)
        {
            var item = solicitud.Obtener(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Solicitud/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = solicitud.Obtener(id);
            if (item != null)
            {
                item.Eliminar();
            }
            return RedirectToAction("Index");
        }

        // POST: Solicitud/Buscar
        [HttpPost]
        public ActionResult Buscar(string criterio)
        {
            var resultados = solicitud.Buscar(criterio);
            return View("Index", resultados);
        }
    }
}