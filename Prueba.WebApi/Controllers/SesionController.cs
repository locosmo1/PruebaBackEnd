using Prueba.Entidad;
using Prueba.Logica;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Prueba.WebApi.Controllers
{
    //HttpGet - Obtener un recurso
    //HttpPost - Crear un nuevo recurso
    //HttpPut - Actualizar  un recurso existente
    //HttpDelete Eliminar un recurso existente
    //Patch - Actualiza un recurso existente solo con cambios
    //Options - Obtener metadatos para interactuar

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Sesion")]
    public class SesionController : ApiController
    {
        LogicaSesion logicaSesion;

        public SesionController()
        {
            logicaSesion = new LogicaSesion();
        }

        //Logica de sesion
        [Route("cerrarSesion")]
        [HttpPost]
        public void cerrarSesion()
        {
            logicaSesion.CerrarSesion();
        }

        [Route("IniciarSesion")]
        [HttpPost]
        public int iniciarSesion([FromBody] UsuarioLogin usuario)
        {
            return logicaSesion.IniciarSesion(usuario);
        }
    }
}
