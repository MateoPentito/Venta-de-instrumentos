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


namespace Ventas_2024.Controllers
{
    //SI NO SE INICIO SESION, ENTONCES NO SE PUEDE INGRESAR A ESTE CONTROLLER Y VISTAS. ESTO SE PROGRAMO EN PROGRAM.CS
    //Si es AUTHORIZE te devuelve al login para iniciar sesion
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }





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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






        //public IActionResult CrearComentario()
        //{
        //    return View();
        //}


        //     Se hace el create de los comentarios
        //[HttpPost]
        //public ActionResult CrearComentario(Comentario comentarios)
        //{
        //    try
        //    {
        //        using (SqlConnection conectar = new(_contexto.Valor))
        //        {
        //            using (SqlCommand cmd = new("sp_registrarComentario", conectar))
        //            {
        //                using System.Data;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = comentarios.Comentarios;
        //                conectar.Open();
        //                cmd.ExecuteNonQuery();
        //                conectar.Close();
        //            }
        //        }
        //        return RedirectToAction("Index", "Home");

        //    }
        //    catch (System.Exception e)
        //    {
        //        ViewData["error"] = e.Message;
        //        return View();
        //    }


        //}



    }
}