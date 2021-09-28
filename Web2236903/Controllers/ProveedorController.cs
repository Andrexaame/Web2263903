using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2236903.Models;


namespace Web.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        [Authorize]
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
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
                var findProveedor = db.proveedor.Find(id);
                return View(findProveedor);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findProveedor = db.proveedor.Find(id);
                    db.proveedor.Remove(findProveedor);
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
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
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

        public ActionResult Edit(proveedor editProveedor)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor proveedor = db.proveedor.Find(editProveedor.id);
                    proveedor.nombre = editProveedor.nombre;
                    proveedor.direccion = editProveedor.direccion;
                    proveedor.telefono = editProveedor.telefono;
                    proveedor.nombre_contacto = editProveedor.nombre_contacto;

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

        public ActionResult uploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            try
            {
                //string para guardar la ruta el archivo
                string filePath = string.Empty;

                //Condicion para saber si llego el archivo
                if(filePath != null)
                {
                    //Ruta para guardar el archivo
                    string path = Server.MapPath("~/Uploads/");

                    //Condicion para saber si existe la ruta

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Obtener nombre del archivo
                    filePath = path + Path.GetFileName(fileForm.FileName);

                    // obtener la extension del archivo
                    string extension = Path.GetExtension(fileForm.FileName);

                    //Guardar el archivo
                    fileForm.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach(string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newproveedor = new proveedor
                            {
                                nombre = row.Split(';')[0],
                                direccion = row.Split(';')[1],
                                telefono = row.Split(';')[2],
                                nombre_contacto = row.Split(';')[3],
                            };

                            using (var db = new inventario2021Entities())
                            {
                                db.proveedor.Add(newproveedor);
                                db.SaveChanges();

                            }
                        }
                    }

                }
                return View();
            }
            catch(Exception Ex)
            {
                ModelState.AddModelError("", "error" + Ex);
                return View();
            }
        }
    }
}