using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ventas_2024.Models;

namespace Ventas_2024.Controllers
{
    //SI NO SE INICIO SESION, ENTONCES NO SE PUEDE INGRESAR A ESTE CONTROLLER Y VISTAS. ESTO SE PROGRAMO EN PROGRAM.CS
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


        //Se hace el create de los comentarios//


        public IActionResult Create()
        {
            return View();
        }
        //////        [HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Create([Bind("Id,Nombre,Fecha,Clave")] Usuario usuario)
        ////{
        ////            if(ModelState.IsValid)
        ////{
        ////_context.Add(usuario);
        ////_context.SaveChangesAsync();
        ////return RedirectToAction(nameof(Index));
        ////}
        ////            return View(usuario);
        //// }
        ////



    }
}