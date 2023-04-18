using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCristoferCastanedaArrieta.Datos;
using ProyectoCristoferCastanedaArrieta.Datos.Repositorio.IRepositorio;
using ProyectoCristoferCastanedaArrieta.Models;
using ProyectoCristoferCastanedaArrieta.Utilidades;

namespace ProyectoCristoferCastanedaArrieta.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepositorio _catRepo;
        public CategoriaController(ICategoriaRepositorio catRepo)
        {
            _catRepo = catRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _catRepo.ObtenerTodos();
            return View(lista);
        }

        //Get
        public IActionResult Crear()
        {            
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _catRepo.Agregar(categoria);
                _catRepo.Grabar();
                return RedirectToAction(nameof(Index));
            }            
            return View(categoria);
        }

        //Get Editar
        public IActionResult Editar(int? Id)
        {
            if (Id==null || Id==0)
            {
                return NotFound();
            }
            var obj = _catRepo.Obtener(Id.GetValueOrDefault());
            if (obj == null) 
            {
                return NotFound(obj);
            }

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Actualizar(categoria);
                _catRepo.Grabar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        //Get Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _catRepo.Obtener(Id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound(obj);
            }

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Categoria categoria)
        {
            if (categoria == null)
            {
                return NotFound();
            }
            _catRepo.Remover(categoria);
            _catRepo.Grabar();
            return RedirectToAction(nameof(Index));
            
        }
    }
}
