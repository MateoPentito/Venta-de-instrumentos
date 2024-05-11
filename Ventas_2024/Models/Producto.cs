using System.ComponentModel.DataAnnotations;

namespace Ventas_2024.Models
{



    public class Producto
    {

        public int Id_Producto { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }


        public void calcularPrecio(string producto,float precio,int cantidad)
        {
            mostrasProducto(producto);
            var total = precio * cantidad;
            Console.WriteLine("El total de tu compra es de: $" + total);
        }


        private void mostrasProducto(string producto)
        {
            Console.WriteLine("Compraste el articulo: "+producto);
        }


    }
}
