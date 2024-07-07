using Microsoft.AspNetCore.Mvc;
using Ventas_2024.Data;
using Ventas_2024.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using VentaDeInstrumentos.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Drawing2D;


namespace Ventas_2024.Controllers
{
    //SI NO SE INICIO SESION, ENTONCES NO SE PUEDE INGRESAR A ESTE CONTROLLER Y VISTAS. ESTO SE PROGRAMO EN PROGRAM.CS
    //Si es AUTHORIZE te devuelve al login para iniciar sesion
    [Authorize]
    public class HomeController : Controller
    {
        //Este metodo conecta la DB
        private readonly DbContext _contexto;
        public HomeController(DbContext context)
        {
            _contexto = context;
        }



        //Este metodo cierra la sesion.
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuarios");
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }








        public IActionResult CrearComentario()
        {
            return View();
        }


        //Este metodo guarda en el store procedure el comentarios
        //Se debe ingresar el nombre de usuario y comentarios 

        //Este atributo indica que el método solo debe ser invocado cuando se realiza una solicitud HTTP POST (por ejemplo, cuando se envía un formulario desde una página web).
        [HttpPost]
        public ActionResult CrearComentario(Comentario com, Usuario use)
        {
            try
            {
                //Se crea una instancia de SqlConnection utilizando la cadena de conexión _contexto.Valor.
                //La cadena de conexión debe estar configurada previamente en la aplicación para conectarse a la base de datos.



                using (SqlConnection conectar = new(_contexto.Valor))
                {
                    using (SqlCommand cmd = new("sp_registrarComentario", conectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = use.User;
                        cmd.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = com.Comentarios;
                        conectar.Open();
                        cmd.ExecuteNonQuery();
                        conectar.Close();
                    }
                }
                return RedirectToAction("Index", "Home");

            }
            catch (System.Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }


        }



        //COMPLETAR
        //Este metodo va a mostrar el comentario que se almaceno en el SQL
        public ActionResult MostrarComentario()
        {
            try
            {

                #region Obtener DataTable de SQL Server - Usuario 1

                string cadena = "server= mateo\\SQLEXPRESS;database=VentaDeInstrumentos;integrated security=true;";
                SqlConnection con = new SqlConnection(cadena);
                string sentencia = "select * from comentario";
                //Se crea una instancia de SqlDataAdapter utilizando la consulta SQL y la conexión establecida previamente.
                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);

                //Contenedor de result set (Resultado del SQL)
                //Se crea un contenedor de resultados (DataTable) para almacenar los datos recuperados de la base de datos.
                DataTable dt = new DataTable();

                //El método Fill del adaptador de datos(da) llena el DataTable con los resultados de la consulta.
                da.Fill(dt);
                //LA VISTA MOSTRAR COMENTARIOS VA A RECIBIR EL DATA TABLE
                #endregion
                return View("ComentariosDelUsuario", dt);

            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
        }





    }
}