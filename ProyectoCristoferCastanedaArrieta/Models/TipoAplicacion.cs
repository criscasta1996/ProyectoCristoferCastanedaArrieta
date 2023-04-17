﻿using System.ComponentModel.DataAnnotations;
namespace ProyectoCristoferCastanedaArrieta.Models
{
    public class TipoAplicacion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El Nombre del Tipo de Aplicacion es obligatorio.")]
        public string Nombre { get; set; }

    }
}
