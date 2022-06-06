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
    public class DomicilioController : ApiController
    {

        LogicaDomicilio logicaDomicilio;
        public DomicilioController()
        {
            logicaDomicilio = new LogicaDomicilio();
        }

        //Logica de domicilio
        [Route("domicilio")]
        [HttpPost]
        public void Post_Domicilio([FromBody] Domicilio domicilio)
        {
            logicaDomicilio.GuardarDomicilio(domicilio);
        }

        [Route("ObtenerDomicilioUsuario")]
        [HttpPost]
        public List<DomicilioUbicacion> ObtenerDomicilioUsuario([FromBody] int idUsuario)
        {
            return logicaDomicilio.ObtenerDomicilioUbicacionUsuario(idUsuario);
        }
    }
}
