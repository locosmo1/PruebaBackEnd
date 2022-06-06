using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class Carrito
    {

        //id_Carrito, Cantidad, id_Producto, id_Cliente
        public Carrito(){}

        public Carrito(int cantidad, int idProducto)
        {
            this.cantidad = cantidad;
            this.idProducto = idProducto;
        }

        public Carrito(int idCarrito, int Cantidad, int idProducto, int idCliente)
        {
            this.idCarrito = idCarrito;
            this.cantidad = Cantidad;
            this.idProducto = idProducto;
            this.idCliente = idCliente;
        }

        public int idCarrito { get; set; }

        public int cantidad { get; set; }

        public int idProducto { get; set; }
        public int idCliente { get; set; }
        
        

        public string tostring()
        {
            return "Id del carrito: " + idCarrito + " " + " Id del cliente: : " + idCliente + " " + " id del producto: " + idProducto + " Cantidad: " + cantidad;
        }

    }
}
