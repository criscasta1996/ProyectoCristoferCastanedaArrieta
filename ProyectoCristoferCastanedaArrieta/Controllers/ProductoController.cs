using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCristoferCastanedaArrieta.Datos;
using ProyectoCristoferCastanedaArrieta.Models;
using ProyectoCristoferCastanedaArrieta.Models.ViewModels;
using System.Linq;
using ProyectoCristoferCastanedaArrieta.Utilidades;

namespace ProyectoCristoferCastanedaArrieta.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria)
                                                        .Include(t => t.TipoAplicacion);
            return View(lista);
        }

        // Get 
        public IActionResult Upsert(int? Id)
        {
            //IEnumerable<SelectListItem> categoriaDropDown = _db.Categoria.Select(c => new SelectListItem
            //{
            //    Text = c.NombreCategoria,
            //    Value = c.Id.ToString()
            //});

            //ViewBag.categoriaDropDown = categoriaDropDown;

            //Producto producto = new Producto();

            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _db.Categoria.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                }),
                TipoAplicacionLista = _db.TipoAplicacion.Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                })
            };

            if (Id == null)
            {
                //Crear un Nuevo Producto
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = _db.Producto.Find(Id);
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productoVM.Producto.Id == 0)
                {
                    //Crear
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productoVM.Producto.ImageUrl = fileName + extension;
                    _db.Producto.Add(productoVM.Producto);
                }
                else
                {
                    //Actualizar
                    var objProducto = _db.Producto.AsNoTracking().FirstOrDefault(p => p.Id == productoVM.Producto.Id);

                    if (files.Count > 0) //Se carga una nueva imagen
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objProducto.ImageUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        //fin borrar imagen anterior

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productoVM.Producto.ImageUrl = fileName + extension;
                    } //Caso contrario si no se carga una nueva imagen
                    else
                    {
                        productoVM.Producto.ImageUrl = objProducto.ImageUrl;
                    }
                    _db.Producto.Update(productoVM.Producto);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Se llenan nuevamente las listas si algo falla
            productoVM.CategoriaLista = _db.Categoria.Select(c => new SelectListItem
            {
                Text = c.NombreCategoria,
                Value = c.Id.ToString()
            });
            productoVM.TipoAplicacionLista = _db.TipoAplicacion.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });

            return View(productoVM);
        }

        //Get
        public IActionResult Eliminar(int? Id)
        {
            if (Id==null || Id==0)
            {
                return NotFound();
            }

            Producto producto = _db.Producto.Include(c => c.Categoria)
                                           .Include(t => t.TipoAplicacion)
                                           .FirstOrDefault(p=>p.Id== Id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Producto producto)
        {
            if(producto == null)
            {
                return NotFound();
            }
            //Eliminar la imagen
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;                        

            //borrar la imagen anterior
            var anteriorFile = Path.Combine(upload, producto.ImageUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            //fin borrar imagen anterior

            _db.Producto.Remove(producto);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
