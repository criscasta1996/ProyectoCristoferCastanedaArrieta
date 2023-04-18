using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCristoferCastanedaArrieta.Datos;
using ProyectoCristoferCastanedaArrieta.Datos.Repositorio.IRepositorio;
using ProyectoCristoferCastanedaArrieta.Models;
using ProyectoCristoferCastanedaArrieta.Utilidades;

namespace ProyectoCristoferCastanedaArrieta.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class TipoAplicacionController : Controller
    {
        private readonly ITipoAplicacionRepositorio _tipoRepo;
        public TipoAplicacionController(ITipoAplicacionRepositorio tipoRepo)
        {
            _tipoRepo = tipoRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<TipoAplicacion> lista = _tipoRepo.ObtenerTodos();
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
        public IActionResult Crear(TipoAplicacion tipoAplicacion)
        {
            if (ModelState.IsValid)
            {
                _tipoRepo.Agregar(tipoAplicacion);
                _tipoRepo.Grabar();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAplicacion);
        }

        //Get Editar
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _tipoRepo.Obtener(Id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound(obj);
            }

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TipoAplicacion tipoAplicacion)
        {
            if (ModelState.IsValid)
            {
                _tipoRepo.Actualizar(tipoAplicacion);
                _tipoRepo.Grabar();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAplicacion);
        }
        //Get Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _tipoRepo.Obtener(Id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound(obj);
            }

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(TipoAplicacion tipoAplicacion)
        {
            if (tipoAplicacion == null)
            {
                return NotFound();
            }
            _tipoRepo.Remover(tipoAplicacion);
            _tipoRepo.Grabar();
            return RedirectToAction(nameof(Index));

        }
    }
}
