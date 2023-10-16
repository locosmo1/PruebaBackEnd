using Prueba.Datos;
using Prueba.Entidad;
using Prueba.Logica;
using System;
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
    [RoutePrefix("api/Cliente")]

    //api/[controlador] llama al nombre del controlador de la misma clase en este caso LogicaCliente
    public class ClienteController : ApiController
    {
        LogicaCliente logicaCliente;
        public ClienteController()
        {
            logicaCliente = new LogicaCliente();
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

    }
}