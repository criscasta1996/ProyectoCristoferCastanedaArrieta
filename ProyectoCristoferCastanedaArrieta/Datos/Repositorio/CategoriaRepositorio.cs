using ProyectoCristoferCastanedaArrieta.Datos.Repositorio.IRepositorio;
using ProyectoCristoferCastanedaArrieta.Models;
using System.Linq.Expressions;

namespace ProyectoCristoferCastanedaArrieta.Datos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepositorio(ApplicationDbContext db):base(db) 
        {        
            _db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var catAnterior = _db.Categoria.FirstOrDefault(c=>c.Id == categoria.Id);
            if(catAnterior != null)
            {
                catAnterior.NombreCategoria = categoria.NombreCategoria;
                catAnterior.MostrarOrden = categoria.MostrarOrden;
            }
        }
    }
}
