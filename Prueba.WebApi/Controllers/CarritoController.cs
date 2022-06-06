using Prueba.Entidad;
using Prueba.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

    public class CarritoController : ApiController
    {
        public LogicaCarrito logicaCarrito;
        public CarritoController()
        {
            logicaCarrito = new LogicaCarrito();
        }


        [Route("agregarCarrito")]
        [HttpPost]
        public void Post_Carrito([FromBody] Carrito carrito)
        {
            logicaCarrito.CrearCarrito(carrito);
        }


        [Route("obtenerCarrito")]
        [HttpPost]
        public IEnumerable<Carrito> obtenerCarrito()
        {
            return logicaCarrito.ObtenerCarrito();
        }


        [Route("obtenerCarrito")]
        [HttpPost]
        public IEnumerable<Carrito> obtenerCarrito(int id_Cliente)
        {
            return logicaCarrito.ObtenerCarrito(id_Cliente);
        }

    }
}