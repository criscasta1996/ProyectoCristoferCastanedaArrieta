using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCristoferCastanedaArrieta.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre del Producto es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Descripcion Corta es Requerida")]
        public string DescripcionCorta { get; set; }

        [Required(ErrorMessage = "Descripcion del Producto es Requerida")]
        public string DescripcionProducto { get; set; }
        [Required(ErrorMessage = "El Precio del Producto es Requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "El Precio debe ser Mayor a Cero")]
        public double Precio { get; set; }


        public string? ImageUrl { get; set; }

        //Foreign Key
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
        public int TipoAplicacionId { get; set; }
        [ForeignKey("TipoAplicacionId")]
        public virtual TipoAplicacion? TipoAplicacion { get; set; }
    }
}
