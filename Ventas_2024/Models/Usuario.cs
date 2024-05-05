using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ventas_2024.Models
{
    public class Usuario
    {

        public int Id_Usuario { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellido { get; set; }
        [Required]
        public DateTime Fecha_Nacimiento { get; set; }
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string? User { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Token { get; set; }
        public byte Estado { get; set; }


        [NotMapped]
        public bool MantenerActivo { get; set; }

    }
}
