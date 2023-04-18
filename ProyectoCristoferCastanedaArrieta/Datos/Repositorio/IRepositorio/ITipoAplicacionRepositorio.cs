using ProyectoCristoferCastanedaArrieta.Models;

namespace ProyectoCristoferCastanedaArrieta.Datos.Repositorio.IRepositorio
{
    public interface ITipoAplicacionRepositorio : IRepositorio<TipoAplicacion>
    {
        void Actualizar(TipoAplicacion tipoAplicacion);
    }
}
