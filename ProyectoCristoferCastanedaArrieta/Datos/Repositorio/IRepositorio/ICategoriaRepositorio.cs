using ProyectoCristoferCastanedaArrieta.Models;

namespace ProyectoCristoferCastanedaArrieta.Datos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        void Actualizar(Categoria categoria);
    }
}
