using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;
using Rotativa;

namespace Web2236903.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int idusuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idusuario).nombre;
            }
        }

        public ActionResult ListaUsuarios()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreCliente(int idcliente)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(idcliente).nombre;
            }
        }

        public ActionResult ListaClientes()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.compra.Add(compra);
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
                var FindCompra = db.compra.Find(id);
                return View(FindCompra);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findCompra = db.compra.Find(id);
                    db.compra.Remove(findCompra);
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
                    compra findcompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findcompra);
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

        public ActionResult Edit(compra editCompra)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    compra compra = db.compra.Find(editCompra.id);
                    compra.fecha = editCompra.fecha;
                    compra.total = editCompra.total;
                    compra.usuario = editCompra.usuario;
                    compra.cliente = editCompra.cliente;

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
        public ActionResult Detalles()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabCompra in db.compra
                            join tabCliente in db.cliente on tabCompra.id_cliente equals tabCliente.id
                            select new Detalles
                            {
                                nombreCliente = tabCliente.nombre,
                                documentoCliente = tabCliente.documento,
                                correoCliente = tabCliente.email,
                                fechaCompra = tabCompra.fecha,
                                totalCompra = tabCompra.total,
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", " error " + ex);
                return View();
            }
        }

        public ActionResult pdfReport()
        {
            return new ActionAsPdf("Detalles") { FileName = "reporte.pdf" };
        }

    }
}
