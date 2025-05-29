using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class UsuarioController : Controller
    {
        private Usuario objUsuario = new Usuario();
        private Tipo_Usuario objTipo = new Tipo_Usuario();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(objUsuario.Listar());
        }

        // GET: Usuario/Detalle/5
        public ActionResult Detalle(int id)
        {
            return View(objUsuario.Obtener(id));
        }

        // GET: Usuario/Crear
        public ActionResult Crear()
        {
            ViewBag.Id_tipousuario = new SelectList(objTipo.Listar(), "Id_tipousuario", "Nombre");
            return View();
        }

        // POST: Usuario/Crear
        [HttpPost]
        public ActionResult Crear(Usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id_tipousuario = new SelectList(objTipo.Listar(), "Id_tipousuario", "Nombre", modelo.Id_tipousuario);
                return View(modelo);
            }

            modelo.Guardar();
            return RedirectToAction("Index");
        }

        // GET: Usuario/Editar/5
        public ActionResult Editar(int id)
        {
            var usuario = objUsuario.Obtener(id);
            ViewBag.Id_tipousuario = new SelectList(objTipo.Listar(), "Id_tipousuario", "Nombre", usuario.Id_tipousuario);
            return View(usuario);
        }

        // POST: Usuario/Editar
        [HttpPost]
        public ActionResult Editar(Usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id_tipousuario = new SelectList(objTipo.Listar(), "Id_tipousuario", "Nombre", modelo.Id_tipousuario);
                return View(modelo);
            }

            modelo.Guardar();
            return RedirectToAction("Index");
        }

        // GET: Usuario/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            return View(objUsuario.Obtener(id));
        }

        // POST: Usuario/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            var usuario = objUsuario.Obtener(id);
            usuario.Eliminar();
            return RedirectToAction("Index");
        }
    }
}