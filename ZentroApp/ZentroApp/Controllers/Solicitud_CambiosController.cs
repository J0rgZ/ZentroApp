using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class Solicitud_CambiosController : Controller
    {
        private Solicitud_Cambios objSolicitud = new Solicitud_Cambios();

        // GET: Solicitud_Cambios
        public ActionResult Index()
        {
            return View(objSolicitud.Listar());
        }

        // GET: Solicitud_Cambios/Detalle/5
        public ActionResult Detalle(int id)
        {
            return View(objSolicitud.Obtener(id));
        }

        // GET: Solicitud_Cambios/Agregar
        public ActionResult Agregar()
        {
            return View();
        }

        // POST: Solicitud_Cambios/Agregar
        [HttpPost]
        public ActionResult Agregar(Solicitud_Cambios modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            modelo.Guardar();
            return RedirectToAction("Index");
        }

        // GET: Solicitud_Cambios/Editar/5
        public ActionResult Editar(int id)
        {
            return View(objSolicitud.Obtener(id));
        }

        // POST: Solicitud_Cambios/Editar
        [HttpPost]
        public ActionResult Editar(Solicitud_Cambios modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            modelo.Guardar();
            return RedirectToAction("Index");
        }

        // GET: Solicitud_Cambios/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            return View(objSolicitud.Obtener(id));
        }

        // POST: Solicitud_Cambios/Eliminar
        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmado(int id)
        {
            var solicitud = objSolicitud.Obtener(id);
            solicitud.Eliminar();
            return RedirectToAction("Index");
        }
    }
}