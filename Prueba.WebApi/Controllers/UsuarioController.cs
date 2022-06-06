using Prueba.Entidad;
using Prueba.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    [RoutePrefix("api/[controlador]")]
    public class UsuarioController : ApiController
    {
        LogicaUsuario logicaUsuario;

        public UsuarioController(LogicaUsuario logicaUsuario)
        {
            this.logicaUsuario = logicaUsuario;
        }

        //Logica del Usuario
        [Route("guardarUsuario")]
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
    }
}
