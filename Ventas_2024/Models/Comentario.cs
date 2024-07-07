using System.ComponentModel.DataAnnotations;
using Ventas_2024.Models;

namespace VentaDeInstrumentos.Models
{
    public class Comentario
    {

        [Required]
        public string? Comentarios { get; set; }
        [Required]
        public string? User { get; set; }

     


    }
}
