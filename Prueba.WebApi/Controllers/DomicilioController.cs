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
    [RoutePrefix("api/Domicilio")]
    public class DomicilioController : ApiController
    {

        LogicaDomicilio logicaDomicilio;
        public DomicilioController()
        {
            logicaDomicilio = new LogicaDomicilio();
        }


        [HttpPost]
        public void Post_Domicilio([FromBody] Domicilio domicilio)
        {
            logicaDomicilio.GuardarDomicilio(domicilio);
        }

        [Route("ObtenerIdUbicacion")]
        [HttpPost]
        public int ObtenerIdUbicacion([FromBody] int idDomicilio)
        {
            return logicaDomicilio.ObtenerIdUbicacion(idDomicilio);
        }


        //Logica de domicilio
        [Route("crearDomicilio")]
        [HttpPost]
        public void CrearDomicilio([FromBody] Domicilio domicilio)
        {
            logicaDomicilio.GuardarDomiciliouUsuario(domicilio);
        }



        [Route("ObtenerDomicilioUsuario")]
        [HttpPost]
        public List<DomicilioUbicacion> ObtenerDomicilioUsuario([FromBody] int idUsuario)
        {
            return logicaDomicilio.ObtenerDomicilioUbicacionUsuario(idUsuario);
        }


        [HttpPut]
        public void ActualizarDomicilio([FromBody] Domicilio domicilio)
        {
            logicaDomicilio.ActualizarDomicilioId(domicilio);
        }



        [HttpDelete]
        public void EliminarDomicilioUbicacion(int id)
        {
            logicaDomicilio.EliminarDomicilio(id);
        }



        [Route("EliminarDomicilioUbicacion")]
        [HttpDelete]
        public void EliminarDomicilioUbicacion(Domicilio domicilio)
        {
            logicaDomicilio.EliminarDomicilioUbicacion(domicilio);
        }

    }
}
