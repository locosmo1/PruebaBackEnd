using System.Web.Http;
using System.Web.Http.Cors;

namespace Prueba.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Linea nueva
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            //Linea nueva
            //config.EnableCors(new AccessPolicyCors());

            // Configuración y servicios de API web
            config.EnableCors(corsAttr);//Linea Original

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            //config.MessageHandlers.Add(new TokenValidationHandler());//linea nueva

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
