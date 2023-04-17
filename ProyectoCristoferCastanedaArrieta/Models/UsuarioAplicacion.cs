using Microsoft.AspNetCore.Identity;

namespace ProyectoCristoferCastanedaArrieta.Models
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
