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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Producto")]
    public class ProductoController : ApiController//ApiController
    {
        //GET: api/Producto
        public LogicaProducto logicaProducto;

        ProductoController()
        {
            logicaProducto = new LogicaProducto();
        }

        [HttpGet]
        public IEnumerable<Producto> ObtenerTodosProductos()
        {
            return logicaProducto.ListarDapper();
        }

        [HttpGet]
        [Route("{idEmpresa}")]
        public IEnumerable<Producto> ObtenerProductoEmpresa(int idEmpresa) //[FromBody] 
        {
            return logicaProducto.ObtenerProductoEmpresa(idEmpresa);
        }


        [Route("ObtenerProductos")]
        [HttpPost]
        public IEnumerable<Producto> obtenerProductosCarrito()
        {
            return logicaProducto.ObtenerProductosCarrito();
        }

        [HttpPost]
        public void NuevoProducto([FromBody] Producto producto)
        {
            logicaProducto.CrearProducto(producto);
        }

        [Route("ObtenerProducto")]
        [HttpPost]
        public Producto PostProduct([FromBody] int id)
        {
            return logicaProducto.ObtenerProducto(id);
        }



        [HttpPut]
        public void ActualizarProducto([FromBody] Producto producto)
        {
            logicaProducto.EditarProducto(producto);
        }



        [HttpDelete]
        public void EliminarProducto([FromBody] int idProducto)
        {
            logicaProducto.EliminarProducto(idProducto);
        }

        [HttpDelete]
        [Route("{idProducto}")]
        public void EliminarProductoId([FromBody] int idProducto)
        {
            logicaProducto.EliminarProducto(idProducto);
        }

        //[HttpGet]
        //public Producto ObtenerProducto([FromBody] int id)
        //{
        //    return logicaProducto.ObtenerProducto(id);
        //}
    }
}
