using Microsoft.AspNetCore.Mvc;
using Ventas_2024.Data;
using System.Data.SqlClient;
using Ventas_2024.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using VentaDeInstrumentos.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace Ventas_2024.Controllers
{

    public class ProductosController : Controller
    {

        //Instancia de solo lectura 
        private readonly DbContext _contexto;
        public ProductosController(DbContext conector)
        {
            _contexto = conector;
        }



        public ActionResult Index()
        {
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



        [Authorize]
        public ActionResult Compra()
        {
            return View();
        }





        public IActionResult CompraRealizada()
        {
            return View();
        }

        //Este metodo registra la compra en la DB
        [HttpPost]
        public ActionResult CompraRealizada(Compra compra)
        {
            try
            {
                using (SqlConnection conectar = new(_contexto.Valor))
                {
                    using (SqlCommand cmd = new("sp_registrarCompra", conectar))
                    {
                        //using System.Data;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = compra.Nombre;
                        cmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = compra.Apellido;

                        cmd.Parameters.Add("@NombreProducto", SqlDbType.VarChar).Value = compra.NombreProducto;
                        cmd.Parameters.Add("@PrecioProducto", SqlDbType.Int).Value = compra.PrecioProducto;

                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = compra.Usuario;
                        cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = compra.Correo;
                        cmd.Parameters.Add("@Domicilio", SqlDbType.VarChar).Value = compra.Domicilio;
                        cmd.Parameters.Add("@Numero", SqlDbType.Int).Value = compra.Numero;
                        cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = compra.Pais;
                        cmd.Parameters.Add("@Provincia", SqlDbType.VarChar).Value = compra.Provincia;
                        cmd.Parameters.Add("@MedioDePago", SqlDbType.VarChar).Value = compra.MedioDePago;
                        cmd.Parameters.Add("@NombreTarjeta", SqlDbType.VarChar).Value = compra.NombreTarjeta;
                        cmd.Parameters.Add("@NumeroTarjeta", SqlDbType.Int).Value = compra.NumeroTarjeta;
                        cmd.Parameters.Add("@Vencimiento", SqlDbType.Int).Value = compra.Vencimiento;
                        cmd.Parameters.Add("@CVV", SqlDbType.Int).Value = compra.CVV;
                        conectar.Open();
                        cmd.ExecuteNonQuery();
                        conectar.Close();
                    }
                }
                return RedirectToAction("Compra", "Productos");

            }
            catch (System.Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }

        }

        //Este metodo muestra la compra de la DB
        public ActionResult mostrarCompra()
        {
            try
            {

                #region Obtener DataTable de SQL Server de la compra

                string cadena = "server= mateo\\SQLEXPRESS;database=VentaDeInstrumentos;integrated security=true;";
                SqlConnection con = new SqlConnection(cadena);
                string sentencia = "SELECT Usuario,Correo,NombreProducto,PrecioProducto,MedioDePago FROM Compra";
                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);

                //Contenedor de result set (Resultado del SQL)
                DataTable dt = new DataTable();

                da.Fill(dt);
                //LA VISTA MOSTRAR COMENTARIOS VA A RECIBIR EL DATA TABLE
                #endregion
                return View("CompraRealizada", dt);

            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
        }
    }
}
