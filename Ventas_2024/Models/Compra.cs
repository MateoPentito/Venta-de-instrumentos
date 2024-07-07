using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VentaDeInstrumentos.Models
{
    public class Compra
    {


        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellido { get; set; }
        [Required]
        public string? Usuario { get; set; }
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string? Domicilio { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string? Pais { get; set; }
        [Required]
        public string? Provincia{ get; set; }
         [Required]
        public string? MedioDePago { get; set; }
        [Required]
        public string? NombreTarjeta { get; set; }
        [Required]
        public int NumeroTarjeta{ get; set; }
        [Required]
        public int Vencimiento { get; set; }
        [Required]
        public int CVV{ get; set; }
        
        [Required]
        public string? NombreProducto { get; set; }
        [Required]
        public int PrecioProducto{ get; set; }
      



    }
}
