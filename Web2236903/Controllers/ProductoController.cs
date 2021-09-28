using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;

namespace Web2236903.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.ToList());
            }
        }

        public static string NombreProveedor(int idproveedor)
        {
            using(var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idproveedor).nombre;
            }
        }

        public ActionResult ListaProveedores()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto.Add(producto);
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
                var findProducto = db.producto.Find(id);
                return View(findProducto);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findProducto = db.producto.Find(id);
                    db.producto.Remove(findProducto);
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
                    producto findProducto = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProducto);
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

        public ActionResult Edit(producto editProducto)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto producto = db.producto.Find(editProducto.id);
                    producto.nombre = editProducto.nombre;
                    producto.percio_unitario = editProducto.percio_unitario;
                    producto.descripcion = editProducto.descripcion;
                    producto.cantidad = editProducto.cantidad;
                    producto.id_proveedor = editProducto.id_proveedor;



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

        public ActionResult Reserva()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reserva
                            {
                                nombreProveedor = tabProveedor.nombre,
                                direccionProveedor = tabProveedor.direccion,
                                telefonoProveedor = tabProveedor.telefono,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    }


}