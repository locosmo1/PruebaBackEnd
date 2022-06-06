using Dapper;
using Prueba.Datos;
using Prueba.Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Prueba.Logica
{
    public class LogicaSesion
    {

        public static int id_Cliente;
        public static int nit_Empresa;
        public static int idUsuario;
        public static Usuario usuarioActual;

        public int IniciarSesion(UsuarioLogin usuario)
        {
            return ObtenerRolUsuario(usuario);
        }

        public int ObtenerRolUsuario(UsuarioLogin usuario)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    String contraseña = Cryptography.EncriptarContraseña(usuario.contraseña);
                    string sql = "select * from Usuario where usuario=@currentUser and contraseña=@currentContraseña";
                    usuarioActual = db.QueryFirstOrDefault<Usuario>(sql, new { currentUser = usuario.usuario, currentContraseña = contraseña });
                    idUsuario = usuarioActual.idUsuario;
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return usuarioActual.idRol;
        }

        public void EnviarMensaje()
        {
            var accountSid = "AC85e533166608cac5b1f44b90ea0a16c1";
            var authToken = "[a02b5d48fb6610fd3d3546d37e1a214e]";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+573508426005"));

            var message = MessageResource.Create(
                body: "Message send from twilio response from Jhonatan Pabon",
                from: new Twilio.Types.PhoneNumber("+573508426005"),
                to: new Twilio.Types.PhoneNumber("+57")
            );
        }

        public void CerrarSesion()
        {
            //var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAMjxQnNpazjJbbWWKitagQN2Be6DKqViY"));
            try
            {
                HttpResponse response = HttpContext.Current.Response;
                HttpRequest request = HttpContext.Current.Request;

                HttpCookieCollection cookies = request.Cookies;
                HttpCookie sessionUserCookie;
                //HttpCookie currentUserCookie;

                sessionUserCookie = request.Cookies["session"];
                sessionUserCookie.Expires = DateTime.Now.AddYears(-1);
                response.Cookies.Add(sessionUserCookie);
                //aCookie.Value = DateTime.Now.ToString();
                //aCookie.Expires = DateTime.Now.AddMinutes(20d);
                //response.Cookies.Add(aCookie);

                HttpCookieCollection cookies2 = HttpContext.Current.Request.Cookies;

                HttpCookieCollection cookies3 = HttpContext.Current.Request.Cookies;

                //if (request.Cookies["session"] != null)
                //{
                //    sessionUserCookie = request.Cookies["session"];
                //    response.Cookies.Remove("session");
                //    response.Cookies.Clear();
                //}
                //sessionUserCookie.Expires = DateTime.Now.AddDays(-10);
                //sessionUserCookie.Value = null;
                //HttpContext.Current.Response.SetCookie(sessionUserCookie);

                //currentUserCookie = request.Cookies["currentUser"];
                //response.Cookies.Remove("currentUser");
                //currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                //currentUserCookie.Value = null;
                //HttpContext.Current.Response.SetCookie(currentUserCookie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
