using Prueba.Entidad;
using Prueba.Logica;
using System.Collections.Generic;
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
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        LogicaUsuario logicaUsuario;

        public UsuarioController()
        {
            this.logicaUsuario = new LogicaUsuario();
        }

        //Logica del Usuario
        [Route("GuardarUsuario")]
        [HttpPost]
        public void Post_Usuario([FromBody] Usuario usuario)
        {
            logicaUsuario.GuardarUsuario(usuario);
        }


        [Route("ObtenerUsuarios")]
        [HttpGet]
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return logicaUsuario.ObtenerUsuarios();
        }

        [Route("ObtenerUsuarioActual")]
        [HttpGet]
        public Usuario ObtenerUsuarioActual()
        {
            return LogicaSesion.usuarioActual;
        }

        [Route("ActualizarUsuario")]
        [HttpGet]
        public void ActualizarUsuario(int idUsuario)
        {
            logicaUsuario.ActualizarUsuario(idUsuario);
        }
    }
}
