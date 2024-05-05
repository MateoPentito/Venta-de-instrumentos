using Microsoft.AspNetCore.Mvc;
using Ventas_2024.Data;
using System.Data.SqlClient;
using Ventas_2024.Models;
using System.Data;

namespace Ventas_2024.Controllers
{
    public class ProductosController : Controller
    {


        private readonly DbContext _contexto;
        public ProductosController(DbContext conector)
        {
            _contexto = conector;
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        //using Ventas_2024.Models;
        public ActionResult Index(Producto producto)
        {
            try
            {
                using (SqlConnection conectar = new(_contexto.Valor))
                {
                    using (SqlCommand cmd = new("sp_registrarCompra", conectar))
                    {
                        //using System.Data;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre",SqlDbType.VarChar).Value = producto.Nombre;
                        cmd.Parameters.Add("@Precio", SqlDbType.Float).Value = producto.Precio;
                        cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = producto.Cantidad;
                        conectar.Open();
                        producto.calcularPrecio(producto.Nombre, producto.Precio, producto.Cantidad);
                        cmd.ExecuteNonQuery();
                        conectar.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewData["error"]=e.Message;
                return View();
            }
            return View();        
        }


        public ActionResult InstrumentoCuerdas()
        {
            return View();
        }

        public ActionResult InstrumentoPercusion()
        {
            return View();
        }
        public ActionResult InstrumentoViento()
        {
            return View();
        }


        public ActionResult ProductoCuerda()
        {
            return View();
        }
        public ActionResult ProductoPercusion()
        {
            return View();
        }

        public ActionResult ProductoViento()
        {
            return View();
        }

        public ActionResult ProductoVario()
        {
            return View();
        }
        
        public ActionResult Compra()
        {
            return View();
        }

        public ActionResult CompraRealizada()
        {
            return View();
        }
    }
}
