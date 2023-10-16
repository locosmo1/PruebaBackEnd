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
    [RoutePrefix("api/Carrito")]

    public class CarritoController : ApiController
    {
        public LogicaCarrito logicaCarrito;
        public CarritoController()
        {
            logicaCarrito = new LogicaCarrito();
        }


        [Route("AgregarCarrito")]
        [HttpPost]
        public void Post_Carrito([FromBody] Carrito carrito)
        {
            logicaCarrito.CrearCarrito(carrito);
        }


        [Route("ObtenerCarrito")]
        [HttpPost]
        public IEnumerable<Carrito> ObtenerCarrito()
        {
            return logicaCarrito.ObtenerCarrito();
        }

    }
}