using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;

namespace Web2236903.Controllers
{
    [Authorize]
    public class UsuarioRolController : Controller
    {
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuariorol.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        public static string NombreUsuario(int idusuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idusuario).nombre;
            }
        }

        public ActionResult ListaUsuario()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreRol(int idroles)
        {
            using (var db = new inventario2021Entities())
            {
                return db.roles.Find(idroles).descripcion;
            }
        }

        public ActionResult ListaRol()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.roles.ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuariorol usuariorol)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.usuariorol.Add(usuariorol);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findUsuarioRol = db.usuariorol.Find(id);
                return View(findUsuarioRol);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findUsuarioRol = db.usuariorol.Find(id);
                    db.usuariorol.Remove(findUsuarioRol);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol findUsuarioRol = db.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(findUsuarioRol);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(usuariorol editUsuarioRol)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol usuariorol = db.usuariorol.Find(editUsuarioRol.id);
                    usuariorol.idUsuario = editUsuarioRol.idUsuario;
                    usuariorol.idRol = editUsuarioRol.idRol;


                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
    }
}