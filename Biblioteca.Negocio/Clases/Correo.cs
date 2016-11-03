using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    public class Correo
    {
        SmtpClient server = new SmtpClient("smtp.gmail.com", 587);

        public Correo()
        {
            server.UseDefaultCredentials = false;
            server.Credentials = new System.Net.NetworkCredential("homerossistem@gmail.com", "homeros2016");
            server.EnableSsl = true;
        }

        public void EnviarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }
    }
}
