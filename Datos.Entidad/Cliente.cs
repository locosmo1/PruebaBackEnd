using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Entidad
{
    public class Cliente
    {

        public Cliente(){}

        public Cliente(int idCliente, string nombreCompleto, string apellidoCompleto, string fechaNacimiento, int idUsuario)
        {
            this.idCliente = idCliente;
            this.nombreCompleto = nombreCompleto;
            this.apellidoCompleto = apellidoCompleto;
            this.fechaNacimiento = fechaNacimiento;
            this.idUsuario = idUsuario;
        }

        public int idCliente { get; set; }

        public String nombreCompleto { get; set; }

        public String apellidoCompleto { get; set; }

        public String fechaNacimiento { get; set; }

        public int idUsuario { get; set; }

        public string tostring()
        {
            return "Id: " + idCliente + " " + " Nombre: " + nombreCompleto + " " + " Apellido: " + apellidoCompleto + " " + " fechaNacimiento: " + fechaNacimiento + " " + " idUsuario: " + idUsuario;
        }
    }
}
