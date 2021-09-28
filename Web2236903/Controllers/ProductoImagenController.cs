using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;

namespace Web2236903.Controllers
{
    [Authorize]
    public class ProductoImagenController : Controller
    {
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }

        public static string NombreProducto(int idproducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idproducto).nombre;
            }
        }

        public ActionResult ListaProductos()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_imagen producto_imagen)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(producto_imagen);
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
                var Findproducto_imagen = db.producto_imagen.Find(id);
                return View(Findproducto_imagen);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findproducto_imagen = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(findproducto_imagen);
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
                    producto_imagen findproducto_imagen = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(findproducto_imagen);
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

        public ActionResult Edit(producto_imagen editproducto_imagen)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_imagen producto_imagen = db.producto_imagen.Find(editproducto_imagen.id);
                    producto_imagen.imagen = editproducto_imagen.imagen;
                    producto_imagen.id_producto = editproducto_imagen.id_producto;


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