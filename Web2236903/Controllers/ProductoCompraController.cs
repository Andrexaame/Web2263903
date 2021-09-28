using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;


namespace Web2236903.Controllers
{
    [Authorize]
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static DateTime FechaCompra(int idcompra)
        {
            using (var db = new inventario2021Entities())
            {
                return db.compra.Find(idcompra).fecha;
            }
        }

        public ActionResult ListaCompras()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.compra.ToList());
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

        public ActionResult Create(producto_compra producto_compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(producto_compra);
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
                var Findproducto_Compra = db.producto_compra.Find(id);
                return View(Findproducto_Compra);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findproducto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findproducto_Compra);
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
                    producto_compra findproducto_compra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findproducto_compra);
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

        public ActionResult Edit(producto_compra editproducto_Compra)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_compra producto_compra = db.producto_compra.Find(editproducto_Compra.id);
                    producto_compra.id_compra = editproducto_Compra.id_compra;
                    producto_compra.id_producto = editproducto_Compra.id_producto;
                    producto_compra.cantidad = editproducto_Compra.cantidad;


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