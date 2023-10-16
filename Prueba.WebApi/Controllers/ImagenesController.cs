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
    [RoutePrefix("api/Imagenes")]
    public class ImagenesController : ApiController
    {

        LogicaImagenes logicaImagenes;

        public ImagenesController()
        {
            logicaImagenes = new LogicaImagenes();
        }

        [Route("recibirImagen")]
        [HttpPost]
        public void PostrecibirImagen([FromBody] ImagenEntidad direccion)
        {
            logicaImagenes.RecibirImagen(direccion);
        }


        //Logica de imagenes
        [Route("ObtenerImagenes")]
        [HttpPost]
        public IEnumerable<string> PostImagenes([FromBody] int id)
        {
            return logicaImagenes.ObtenerImagenes(id);
        }


        [Route("actualizarImagenes")]
        [HttpPost]
        public void PostActualizarImagenes(Imagen imagen, int id_Producto)
        {
            logicaImagenes.ActualizarImagen(imagen, id_Producto);
        }


        [Route("ObtenerImagenesProducto/{idProducto}")]
        [HttpGet]
        public List<Imagen> ObtenerImagenesProducto(int idProducto)
        {
            return logicaImagenes.ObtenerImagenesProducto(idProducto);
        }


    }
}
