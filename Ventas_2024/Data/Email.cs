using System.Net;
using System.Net.Mail;
using Ventas_2024.Models;
namespace Ventas_2024.Data


{
    public class Email
    {

        public void Enviar(string correo,string token)
        {
            Correo(correo, token);
        }

        void Correo(string correo_receptor,string token)
        {
            string correo_emisor = "database_ventas@hotmail.com";
            string clave_emisor = "guns1234";

            MailAddress emisor = new(correo_emisor);
            MailAddress receptor = new(correo_receptor);

            MailMessage email = new MailMessage(emisor, receptor);
            email.Subject = "Confirmación de validacion de cuenta";
            email.Body =
                
                
                
                "Hola! "+ correo_receptor +", para activar tu cuenta debes hacer click en el siguiente enlace: http://localhost:27019/Usuarios/Token?valor="+ token;
           


            SmtpClient smtp = new ();
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(correo_emisor, clave_emisor);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (System.Exception)
            {
                throw;
            }

        }



    }
}
