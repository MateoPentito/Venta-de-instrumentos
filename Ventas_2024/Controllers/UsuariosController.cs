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

namespace Ventas_2024.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly DbContext _contexto;

        public UsuariosController(DbContext contexto)
        {
            _contexto = contexto;
        }


        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            try
            {
                using (SqlConnection conection = new(_contexto.Valor))
                {
                    using (SqlCommand cmd = new("sp_validar_cuenta", conection))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.User;
                        cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = usuario.Password;
                        conection.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Usuario"] != null && usuario.User != null)
                            {
                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier,usuario.User)
                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = usuario.MantenerActivo;

                                if (!usuario.MantenerActivo)
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                }
                                else
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
                                }
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Credenciales incorrectas o cuenta no registrada.";
                            }
                            conection.Close();
                        }
                        return View();
                    }

                }

            }catch(System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }


        }






        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(Usuario name)
        {
            try
            {
                using (SqlConnection conection = new SqlConnection(_contexto.Valor))
                {
                    using (SqlCommand cmd = new("sp_registrarUsuario", conection))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = name.Nombre;
                        cmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = name.Apellido;
                        cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = name.Fecha_Nacimiento;
                        cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = name.Correo;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = name.User;
                        cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = name.Password;
                        var token = Guid.NewGuid();
                        cmd.Parameters.Add("@Token", SqlDbType.VarChar).Value = token.ToString();
                        cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = 0;
                        conection.Open();
                        cmd.ExecuteNonQuery();

                        Email email = new();

                        if(name.Correo != null)
                        {
                            email.Enviar(name.Correo, token.ToString());
                        }

                        conection.Close();

                    }
                }

                return RedirectToAction("Token", "Usuarios");
            }
            catch (System.Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }


        }


        public ActionResult Token()
        {
            string token = Request.Query["valor"];

            if(token != null)
            {
                try
                {
                    using (SqlConnection conection = new(_contexto.Valor))
                    {
                        using (SqlCommand cmd = new("sp_validarUsuario",conection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Token", SqlDbType.VarChar).Value = token;
                            conection.Open();
                            cmd.ExecuteNonQuery();
                            ViewData["mensaje"] = "Su cuenta ha sido validada exitosamente!";
                            conection.Close();
                        }
                    }
                    return View();

                }
                catch (System.Exception e)
                {
                    ViewData["mensaje"] = e.Message;
                    return View();
                }
            }
            else
            {
                Email correo = new();
                ViewData["mensaje"] = "Verifique su correo para activar su cuenta"; 
                return View();
            }
        }


        public IActionResult Logout()
        {
            return View();
        }









    }


}