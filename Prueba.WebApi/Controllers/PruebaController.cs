

using Prueba.Datos;
using Prueba.Entidad;
using Prueba.Logica;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Cors;

//Logica donde estan los metodos que voy a utilizar
//Formatear documento ctrl + e d

namespace Prueba.WebApi.Controllers
{

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/prueba")]

    //HttpGet - Obtener un recurso
    //HttpPost - Crear un nuevo recurso
    //HttpPut - Actualizar  un recurso existente
    //HttpDelete Eliminar un recurso existente
    //Patch - Actualiza un recurso existente solo con cambios
    //Options - Obtener metadatos para interactuar
    public class PruebaController : ApiController
    {
        public SqlConnection conexion;

        public LogicaCarrito logicaCarrito;
        public LogicaCliente logicaCliente;
        public LogicaDomicilio logicaDomicilio;
        public LogicaImagenes logicaImagenes;
        public LogicaProducto logicaProducto;
        public LogicaSesion logicaSesion;
        public LogicaUsuario logicaUsuario;

        public PruebaController()
        {
            logicaCarrito = new LogicaCarrito();
            logicaCliente = new LogicaCliente();
            logicaDomicilio = new LogicaDomicilio();
            logicaImagenes = new LogicaImagenes();
            logicaProducto = new LogicaProducto();
            logicaSesion = new LogicaSesion();
            logicaUsuario = new LogicaUsuario();
        }


        //Logica de sesion
        [Route("cerrarSesion")]
        [HttpPost]
        public void cerrarSesion()
        {
            logicaSesion.CerrarSesion();
        }

        [Route("iniciarSesion")]
        [HttpPost]
        public int iniciarSesion([FromBody] UsuarioLogin usuario)
        {
            return logicaSesion.IniciarSesion(usuario);
        }





        //Logica de Producto
        [Route("obtenerProductos")]
        [HttpPost]
        public IEnumerable<Producto> obtenerProductosCarrito()
        {
            return logicaProducto.ObtenerProductosCarrito();
        }


        [HttpGet]
        public IEnumerable<Producto> GetAllProducts()
        {
            return logicaProducto.ListarDapper();
        }


        [Route("producto")]
        [HttpPost]
        public Producto PostProduct([FromBody] int id)
        {
            return logicaProducto.ObtenerProducto(id);
        }


        [Route("CreateProducto")]
        [HttpPost]
        public void NuevoProducto([FromBody] Producto producto)
        {
            logicaProducto.CrearProducto(producto);
        }

        [Route("ObtenerProductosEmpresa")]
        [HttpPost]
        public IEnumerable<Producto> ObtenerProductoEmpresa([FromBody] int idEmpresa)
        {
            return logicaProducto.ObtenerProductoEmpresa(idEmpresa);
        }






        //Logica de carrito
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





        //Logica de imagen del producto
        [Route("recibirImagen")]
        [HttpPost]
        public void PostrecibirImagen([FromBody] ImagenEntidad direccion)
        {
            logicaImagenes.RecibirImagen(direccion);
        }


        //Logica de imagenes
        [Route("imagenes")]
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

        [Route("ObtenerImagenesProducto")]
        [HttpGet]
        public List<Imagen> ObtenerImagenesProducto([FromBody] int idProducto)
        {
            return logicaImagenes.ObtenerImagenesProducto(idProducto);
        }





        //Logica del cliente
        [Route("guardarCliente")]
        [HttpPost]
        public void Post_Cliente([FromBody] Cliente cliente)
        {
            logicaCliente.GuardarCliente(cliente);
        }



        [Route("obtenerContraseña")]
        [HttpPost]
        public String obtenerContraseñaCifrada([FromBody] string contraseña)
        {
            return Cryptography.EncriptarContraseña(contraseña);
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


//Ejemplos sobre como usar post, put, delete
//[HttpPost]
//public void Post([FromBody] Producto producto) //Insertar un nuevo producto
//{
//    logica.InsertarDapper(producto);
//}


//[HttpPut] //Modificar o actualizar
//public void Put([FromBody] Producto producto)
//{
//    logica.ActualizarDapper(producto);
//}


//[HttpDelete] //Eliminar un recurso
//public void Delete(int id) //[FromBody]
//{
//    logica.Borrar(id);
//}




/*public static void Register(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute("https://localhost:44370/api/prueba/", "*", "*");
            config.EnableCors(corsAttr);
        }*/


//[Route("login")]
//[HttpPost]
//public bool Post_Login([FromBody] Cliente cliente) //Insertar los datos del cliente para iniciar sesion
//{
//    return logica.IniciarSesion(cliente);
//}

//[HttpGet]
//public IHttpActionResult GetProduct(int id) //Obtener 1 producto por identificacion
//{
//    List<Producto> productos = new List<Producto>();
//    Producto producto = productos.FirstOrDefault((p) => p.id_Producto == id);
//    if (producto == null)
//    {
//        return NotFound();
//    }
//    return Ok(producto);
//}